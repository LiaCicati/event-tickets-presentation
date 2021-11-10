using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service
{
    public class MQCreditCardService : MQService, ICreditCardService
    {
        public MQCreditCardService(IModel channel) : base(channel)
        {
        }

        public Task<CreditCard> AddCreditCardAsync(CreditCard creditCard)
        {
            return SendAndConvertAsync<CreditCard, CreditCard>("addCreditCard", creditCard);
        }

        public Task RemoveCreditCardAsync(CreditCard creditCard)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CreditCard>> GetCreditCardsForUserAsync(long ownerId)
        {
            return SendAndConvertAsync<List<CreditCard>, long>("getCreditCards", ownerId);
        }
    }
}