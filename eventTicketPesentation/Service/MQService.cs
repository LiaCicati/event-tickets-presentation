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
        private IModel channel;
        private string replyQueueName;
        private EventingBasicConsumer consumer;

        public MQService(IModel channel)
        {
            this.channel = channel;
            var queue = channel.QueueDeclare("", false, false, false, null);
            this.replyQueueName = queue.QueueName;
            this.consumer = new EventingBasicConsumer(channel);
        }

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