using System.Text.Json.Serialization;

namespace eventTicketPesentation.Service.dto
{
    public class BookTicketDTO
    {
        [JsonPropertyName("eventId")]
        public long EventId { get; set; }
        
        [JsonPropertyName("paymentId")]
        public long PaymentId { get; set; }
        
        [JsonPropertyName("buyerId")] public long BuyerId { get; set; }
        
        [JsonPropertyName("noOfTickets")] public int NoOfTickets { get; set; }
    }
}