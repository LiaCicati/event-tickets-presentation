using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Service
{
    public interface ICreditCardService
    {
        Task<CreditCard> AddCreditCardAsync(CreditCard creditCard);
        Task RemoveCreditCardAsync(CreditCard creditCard);
        Task<List<CreditCard>> GetCreditCardsForUserAsync(long ownerId);
    }
}