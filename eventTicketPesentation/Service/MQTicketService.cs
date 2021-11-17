using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service
{
    public class MQTicketService : MQService, ITicketService
    {
        public MQTicketService(IModel channel) : base(channel)
        {
        }

        public Task<Ticket> BookTicketAsync(BookTicketDTO ticketDto)
        {
            return SendAndConvertAsync<Ticket, BookTicketDTO>("bookTicket", ticketDto);
        }

        public Task<List<TicketWithEventDTO>> GetTicketsForUserAsync(long userId)
        {
            var res = SendAndConvertAsync<List<TicketWithEventDTO>, long>("getTicketsForUser", userId);
            return res;
        }
    }
}