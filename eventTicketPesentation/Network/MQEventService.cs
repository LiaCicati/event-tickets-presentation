using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using eventTicketPesentation.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eventTicketPesentation.Network
{
    public class MQEventService: IEventService
    {
        private IModel channel;
        private string replyQueueName;
        private EventingBasicConsumer consumer;
        
        private string logicTierQueue = "eventTicketLogicQueue";

        public MQEventService(IConnection connection)
        {
            channel = connection.CreateModel();
            var queue = channel.QueueDeclare("", false, false, false, null);
            replyQueueName = queue.QueueName;
            consumer = new EventingBasicConsumer(channel);
        }
        
        public List<Event> GetAllEvents()
        {
            var props = channel.CreateBasicProperties();
            props.CorrelationId = Guid.NewGuid().ToString();
            props.ReplyTo = replyQueueName;
            
            var respQueue = new BlockingCollection<string>();
            
            var msg = Encoding.UTF8.GetBytes("getAllEvents");
            channel.BasicPublish("", logicTierQueue, props, msg);

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

            return JsonSerializer.Deserialize<List<Event>>(respQueue.Take());
        }
    }
}