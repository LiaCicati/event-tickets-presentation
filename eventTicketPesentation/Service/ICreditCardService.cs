using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;

namespace eventTicketPesentation.Service
{
    public interface ICreditCardService
    {
        Task<CreditCard> AddCreditCardAsync(CreateCreditCardDTO creditCardDto);
        Task RemoveCreditCardAsync(CreditCard creditCard);
        Task<List<CreditCard>> GetCreditCardsForUserAsync(long ownerId);
    }
}