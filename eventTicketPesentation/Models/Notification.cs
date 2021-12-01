using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eventTicketPesentation.Models
{
    public class Notification
    {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("timestamp")] public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"EVENT {Id} {Title} {Message} {Timestamp}";
        }
    }
}