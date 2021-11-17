using System.Text.Json.Serialization;

namespace eventTicketPesentation.Service.dto
{
    public class BookTicketDTO
    {
        [JsonPropertyName("buyerId")]
        public long BuyerId { get; set; }
        [JsonPropertyName("eventId")]
        public long EventId { get; set; }
        public int nrOfTickets { get; set; }
    }
}