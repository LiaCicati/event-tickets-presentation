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

        [JsonPropertyName("nrOfTickets")]
        [Required]
        public int NrOfTickets { get; set; }

        [JsonPropertyName("category")]
        [Required]
        public string Category { get; set; }

        [JsonPropertyName("isCancelled")] public bool IsCancelled { get; set; }

        [JsonPropertyName("dateTime")]
        [Required]
        public DateTime DateTime { get; set; }

        public Address Address { get; set; }
        [JsonPropertyName("price")] public double Price { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Description}";
        }
    }
}