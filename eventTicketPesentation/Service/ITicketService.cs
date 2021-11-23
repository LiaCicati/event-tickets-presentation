using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;

namespace eventTicketPesentation.Service
{
    public interface ITicketService
    {
        Task<List<Ticket>> BookTicketsAsync(BookTicketDTO ticketDto);
        Task<List<TicketGroupDTO>> GetTicketsForUserAsync(long userId);
    }
}