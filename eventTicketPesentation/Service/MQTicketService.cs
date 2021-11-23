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

        public Task<List<Ticket>> BookTicketsAsync(BookTicketDTO ticketDto)
        {
            return SendAndConvertAsync<List<Ticket>, BookTicketDTO>("bookTicket", ticketDto);
        }

        public Task<List<TicketGroupDTO>> GetTicketsForUserAsync(long userId)
        {
            return SendAndConvertAsync<List<TicketGroupDTO>, long>("getTicketsForUser", userId);
        }
    }
}