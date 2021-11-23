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
        private IModel
            channel; // represents an AMQP 0-9-1 channel, and provides most of the operations (protocol methods)

        private string replyQueueName;
        private EventingBasicConsumer consumer; // a consumer implementation built around C# event handlers.
        private string logicTierExchange;

        private Dictionary<string, TaskCompletionSource<byte[]>> _completionSources;

        protected MQService(IModel channel)
        {
            this.channel = channel;
            var queue = channel.QueueDeclare("", false, false, false, null);
            this.replyQueueName = queue.QueueName;
            this.consumer = new EventingBasicConsumer(channel); // sets the Model property to the given value.
            this.logicTierExchange = "eventTicketsLogicTier";
            channel.ExchangeDeclare(logicTierExchange, "direct", true);

            _completionSources = new Dictionary<string, TaskCompletionSource<byte[]>>();
      
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("RECEIVED REQUEST: " + Encoding.UTF8.GetString(ea.Body.ToArray()));
                
                _completionSources[ea.BasicProperties.CorrelationId].SetResult(ea.Body.ToArray());
                _completionSources.Remove(ea.BasicProperties.CorrelationId);
            };
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
            
            channel.BasicConsume(replyQueueName, true, consumer);

            return completionSource.Task;
        }

        protected Task<byte[]> SendRequestAsync(string queue)
        {
            return SendRequestAsync(queue, new byte[] { });
        }

        protected async Task<TR> SendAndConvertAsync<TR, TP>(string queue, TP arg)
        {
            var req = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(arg));

            var resp = await SendRequestAsync(queue, req);

            return JsonSerializer.Deserialize<TR>(
                Encoding.UTF8.GetString(resp));
        }

        protected async Task<TR> SendAndConvertAsync<TR>(string queue)
        {
            var resp = await SendRequestAsync(queue);

            return JsonSerializer.Deserialize<TR>(
                Encoding.UTF8.GetString(resp));
        }
    }
}