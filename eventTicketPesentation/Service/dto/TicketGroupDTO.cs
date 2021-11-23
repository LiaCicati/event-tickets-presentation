using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Service.dto
{
    public class TicketGroupDTO
    {
        [JsonPropertyName("eventId")] public long EventId { get; set; }
        [JsonPropertyName("nameOfEvent")] public string NameOfEvent { get; set; }
        [JsonPropertyName("dateTimeOfEvent")] public DateTime TimeOfTheEvent { get; set; }
        [JsonPropertyName("thumbnail")] public string Thumbnail { get; set; }
        [JsonPropertyName("location")] public string Location { get; set; }
        [JsonPropertyName("ticketPrice")] public double TicketPrice { get; set; }
        
        [JsonPropertyName("tickets")] public List<Ticket> Tickets { get; set; }

        public int NoOfTickets()
        {
            return Tickets.Count;
        }

        public double TotalPrice()
        {
            return NoOfTickets() * TicketPrice;
        }
    }
}