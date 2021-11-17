using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service
{
    public class MQPaymentService : MQService, IPaymentService
    {
        public MQPaymentService(IModel channel) : base(channel)
        {
        }

        public Task<Payment> MakePaymentAsync(MakePaymentDTO makePaymentDto)
        {
            return SendAndConvertAsync<Payment, MakePaymentDTO>("makePayment", makePaymentDto);
        }

        public Task<Payment> FindForTicketAsync(FindTicketDTO findTicketDto)
        {
            return SendAndConvertAsync<Payment, FindTicketDTO>("findPaymentForTicket", findTicketDto);
        }
    }
}