using System.Collections.Generic;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Network
{
    public interface IEventService
    {
        List<Event> GetAllEvents();

        Event AddEvent(Event e);
    }
}