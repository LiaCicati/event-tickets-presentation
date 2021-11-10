using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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

        public Ticket BookTicket(BookTicketDTO ticketDto)
        {
            var msg = Serialize(ticketDto);
            return sendRequest<Ticket>("bookTicket", msg);
        }

        public List<Ticket> GetTicketsForUser(long userId)
        {
            var res = sendRequest<List<Ticket>>("getTicketsForUser", BitConverter.GetBytes(userId));
            Console.WriteLine(res);
            return res;
        }
    }
}