using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Service
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();

        Task<Event> AddEventAsync(Event e);
        Task<Event> GetEventByIdAsync(long id);
    }
}