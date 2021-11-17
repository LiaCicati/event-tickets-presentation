using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;

namespace eventTicketPesentation.Service
{
    public interface IPaymentService
    {
        Task<Payment> MakePaymentAsync(MakePaymentDTO makePaymentDto);
        Task<Payment> FindForTicketAsync(FindTicketDTO findTicketDto);
    }
}