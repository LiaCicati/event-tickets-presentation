using System.Collections.Generic;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;

namespace eventTicketPesentation.Service
{
    public interface ITicketService
    {
        Ticket BookTicket(BookTicketDTO ticketDto);
        List<Ticket> GetTicketsForUser(long userId);
    }
}