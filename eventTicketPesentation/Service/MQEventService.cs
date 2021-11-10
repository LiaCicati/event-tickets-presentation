using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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


        public Task<List<Event>> GetAllEventsAsync()
        {
            return SendAndConvertAsync<List<Event>>("getAllEvents");
        }

        public Task<Event> AddEventAsync(Event e)
        {
            return SendAndConvertAsync<Event, Event>("addEvent", e);
        }

        public Task<Event> GetEventByIdAsync(long id)
        {
            return SendAndConvertAsync<Event, long>("getEventById", id);
        }
    }
}