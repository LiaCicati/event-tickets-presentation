using System;
using System.Text.Json.Serialization;

namespace eventTicketPesentation.Service.dto
{
    public class TicketWithEventDTO
    {
        [JsonPropertyName("eventId")] public long EventId { get; set; }
        [JsonPropertyName("ticketNumber")] public string TicketNumber { get; set; }
        [JsonPropertyName("nameOfEvent")] public string NameOfEvent { get; set; }
        [JsonPropertyName("dateTimeOfEvent")] public DateTime DateTimeOfEvent { get; set; }
        [JsonPropertyName("location")] public string Location { get; set; }
        [JsonPropertyName("price")] public double Price { get; set; }
        [JsonPropertyName("numberOfTickets")] public int NumberOfTickets { get; set; }
        [JsonPropertyName("thumbnail")] public string Thumbnail { get; set; }
    }
}