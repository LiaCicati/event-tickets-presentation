using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eventTicketPesentation.Service.MQ
{
    public class MQEventService : MQService, IEventService
    {
        public MQEventService(IModel channel) : base(channel)
        {
        }


        public Task<List<Event>> GetUpcomingEventsAsync()
        {
            return SendAndConvertAsync<List<Event>>("getUpcomingEvents");
        }

        public Task<Event> AddEventAsync(Event e)
        {
            return SendAndConvertAsync<Event, Event>("addEvent", e);
        }

        public Task<Event> GetEventByIdAsync(long id)
        {
            return SendAndConvertAsync<Event, long>("getEventById", id);
        }

        public Task<Event> UpdateEventAsync(Event e)
        {
            return SendAndConvertAsync<Event, Event>("updateEvent", e);
        }

        public Task<Event> CancelEventAsync(long id)
        {
            return SendAndConvertAsync<Event, long>("cancelEvent", id);
        }

        public Task<List<Event>> GetUpcomingEventsByCategoryAsync(long id)
        {
            return SendAndConvertAsync<List<Event>,long>("getUpcomingEventsByCategory", id);
        }
        
        public Task<List<Event>> GetUpcomingEventsByLocationAsync(string location)
        {
            return SendAndConvertAsync<List<Event>, string>("getUpcomingEventsByLocation", location);
        }
    }
}