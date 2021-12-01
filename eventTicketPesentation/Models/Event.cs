using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eventTicketPesentation.Models
{
    public class Event
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [Required(ErrorMessage = "Name Field is Required")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Field is Required")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("location")]
        [Required]
        public string Location { get; set; }

        [JsonPropertyName("thumbnail")]
        [Required]
        public string Thumbnail { get; set; }

        [JsonPropertyName("availableTickets")]
        [Required]
        public int AvailableTickets { get; set; }

        [JsonPropertyName("category")]
       
        public string Category { get; set; }

        [JsonPropertyName("isCancelled")] public bool IsCancelled { get; set; }

        [JsonPropertyName("timeOfTheEvent")]
        [Required]
        public DateTime TimeOfTheEvent { get; set; }
        
        [JsonPropertyName("ticketPrice")] public double TicketPrice { get; set; }

        [JsonPropertyName("organizerId")]
        public long OrganizerId { get; set; }
        
        [JsonPropertyName("bookedTickets")]
        public int BookedTickets { get; set; }

       [JsonIgnore] public int RemainingTickets => AvailableTickets - BookedTickets;

        public override string ToString()
        {
            return $"{Id} {Name} {Description}";
        }
    }
}