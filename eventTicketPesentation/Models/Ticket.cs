using System.Text.Json.Serialization;

namespace eventTicketPesentation.Models
{
    public class Ticket
    {
        [JsonPropertyName("ticketNr")] public string TicketNr { get; set; }
        [JsonPropertyName("eventId")] public long EventId { get; set; }
        [JsonPropertyName("paymentId")] public long paymentId { get; set; }
        [JsonPropertyName("buyerId")] public long BuyerId { get; set; }
    }
}