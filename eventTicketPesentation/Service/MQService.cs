using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eventTicketPesentation.Service
{
    public class MQService
    {
        private static readonly string logicTierExchange = "eventTicketsLogicTier";
        private static readonly string dlx = "deadLetterExchange";
        
        
        private IModel channel; // represents an AMQP 0-9-1 channel, and provides most of the operations (protocol methods)

        private string replyQueueName;

        private Dictionary<string, TaskCompletionSource<byte[]>> _completionSources;

        protected MQService(IModel channel)
        {
            this.channel = channel;
            _completionSources = new Dictionary<string, TaskCompletionSource<byte[]>>();
            
            channel.ExchangeDeclare(logicTierExchange, "direct", true);
            channel.ExchangeDeclare(dlx, "fanout", true);
            
            this.replyQueueName = SetupQueue((model, ea) =>
            {
                Console.WriteLine("RECEIVED REQUEST: " + Encoding.UTF8.GetString(ea.Body.ToArray()));
                
                _completionSources[ea.BasicProperties.CorrelationId]
                    .SetResult(ea.Body.ToArray());
                _completionSources.Remove(ea.BasicProperties.CorrelationId);
            });

            var dlqName = SetupQueue((model, ea) =>
            {
                var msg = Deserialize<String>(ea.Body.ToArray());

                Console.WriteLine("RECEIVED DEAD LETTER: " + msg);

                _completionSources[ea.BasicProperties.CorrelationId]
                    .SetException(new Exception(msg));
                _completionSources.Remove(ea.BasicProperties.CorrelationId);
            });
            channel.QueueBind(dlqName, dlx, "");
        }

        private string SetupQueue(EventHandler<BasicDeliverEventArgs> messageHandler)
        {
            var queue = channel.QueueDeclare("", false, false, false, null);
            var consumer = new EventingBasicConsumer(channel); // sets the Model property to the given value.
            consumer.Received += messageHandler;
            channel.BasicConsume(queue.QueueName, true, consumer);

            return queue.QueueName;
        }

        protected Task<byte[]> SendRequestAsync(string queue, byte[] msg)
        {
            var props = channel.CreateBasicProperties();
            props.CorrelationId = Guid.NewGuid().ToString();
            props.ReplyTo = replyQueueName;

            Console.WriteLine("SENDING " + Encoding.UTF8.GetString(msg));

            var completionSource = new TaskCompletionSource<byte[]>();
            _completionSources[props.CorrelationId] = completionSource;

            channel.BasicPublish(logicTierExchange, queue, props, msg);

            return completionSource.Task;
        }

        protected Task<byte[]> SendRequestAsync(string queue)
        {
            return SendRequestAsync(queue, new byte[] { });
        }

        private T Deserialize<T>(byte[] arr)
        {
            return JsonSerializer.Deserialize<T>(
                Encoding.UTF8.GetString(arr));
        }

        protected async Task<TR> SendAndConvertAsync<TR, TP>(string queue, TP arg)
        {
            var req = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(arg));

            var resp = await SendRequestAsync(queue, req);

            return Deserialize<TR>(resp);
        }

        protected async Task<TR> SendAndConvertAsync<TR>(string queue)
        {
            var resp = await SendRequestAsync(queue);

            return JsonSerializer.Deserialize<TR>(
                Encoding.UTF8.GetString(resp));
        }
    }
}