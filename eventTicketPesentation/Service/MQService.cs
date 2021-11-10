using System;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eventTicketPesentation.Service
{
    public class MQService
    {
        private IModel channel; // represents an AMQP 0-9-1 channel, and provides most of the operations (protocol methods)
        private string replyQueueName;
        private EventingBasicConsumer consumer; // a consumer implementation built around C# event handlers.

        protected MQService(IModel channel)
        {
            this.channel = channel;
            var queue = channel.QueueDeclare("", false, false, false, null);
            this.replyQueueName = queue.QueueName;
            this.consumer = new EventingBasicConsumer(channel); // sets the Model property to the given value.
        }

        // publish/subscribe - asynchronous update 
        // request/reply pattern (Remote Procedure Call) - immediate response 
        protected Task<byte[]> SendRequestAsync(string queue, byte[] msg)
        {
            var props = channel.CreateBasicProperties();
            props.CorrelationId = Guid.NewGuid().ToString();
            props.ReplyTo = replyQueueName;

            channel.BasicPublish("", queue, props, msg);

            var completionSource = new TaskCompletionSource<byte[]>();

            EventHandler<BasicDeliverEventArgs> handler = null;
            handler = (model, ea) =>
            {
                if (ea.BasicProperties.CorrelationId == props.CorrelationId)
                {
                    consumer.Received -= handler;
                    completionSource.SetResult(ea.Body.ToArray());
                }
            };
            consumer.Received += handler;

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