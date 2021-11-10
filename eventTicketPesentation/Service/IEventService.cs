using System.Collections.Generic;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Service
{
    public interface IEventService
    {
        List<Event> GetAllEvents();

        Event AddEvent(Event e);
        Event GetEventById(long id);
    }
}