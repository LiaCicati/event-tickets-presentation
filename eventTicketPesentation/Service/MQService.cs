using System;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eventTicketPesentation.Service
{
    public class MQService
    {
        private IModel channel; // represents an AMQP 0-9-1 channel, and provides most of the operations (protocol methods)
        private string replyQueueName;
        private EventingBasicConsumer consumer; // a consumer implementation built around C# event handlers.

        public MQService(IModel channel)
        {
            this.channel = channel;
            var queue = channel.QueueDeclare("", false, false, false, null);
            this.replyQueueName = queue.QueueName;
            this.consumer = new EventingBasicConsumer(channel); // sets the Model property to the given value.
        }

        // publish/subscribe - asynchronous update 
        // request/reply pattern (Remote Procedure Call) - immediate response 
        protected T sendRequest<T>(string queue, byte[] msg)
        {
            var props = channel.CreateBasicProperties();
            props.CorrelationId = Guid.NewGuid().ToString();
            props.ReplyTo = replyQueueName;

            var respQueue = new BlockingCollection<string>();

            channel.BasicPublish("", queue, props, msg);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                Console.WriteLine(response);

                if (ea.BasicProperties.CorrelationId == props.CorrelationId)
                {
                    respQueue.Add(response);
                }
            };

            channel.BasicConsume(replyQueueName, true, consumer);

            return JsonSerializer.Deserialize<T>(respQueue.Take());
        }
        
        protected T sendRequest<T>(string queue)
        {
            return sendRequest<T>(queue, new byte[] { });
        }
    }
}