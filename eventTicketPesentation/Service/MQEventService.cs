using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using eventTicketPesentation.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eventTicketPesentation.Service
{
    public class MQEventService : MQService, IEventService
    {
        public MQEventService(IModel channel) : base(channel)
        {
        }


        public List<Event> GetAllEvents()
        {
            return sendRequest<List<Event>>("getAllEvents");
        }

        public Event AddEvent(Event e)
        {
            var msg = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(e));
            return sendRequest<Event>("addEvent", msg);
        }
    }
}