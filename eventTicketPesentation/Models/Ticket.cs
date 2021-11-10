using System.Text.Json.Serialization;

namespace eventTicketPesentation.Models
{
    public class Ticket
    {
        [JsonPropertyName("buyerId")] public long BuyerId { get; set; }
        [JsonPropertyName("eventId")] public long EventId { get; set; }
        [JsonPropertyName("ticketNr")] public string TicketNr { get; set; }
        [JsonPropertyName("price")] public double Price { get; set; }
        [JsonPropertyName("nrOfTickets")] public int NrOfTickets { get; set; }
    }
}