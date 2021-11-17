using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;

namespace eventTicketPesentation.Service
{
    public interface ITicketService
    {
        Task<Ticket> BookTicketAsync(BookTicketDTO ticketDto);
        Task<List<TicketWithEventDTO>> GetTicketsForUserAsync(long userId);
    }
}