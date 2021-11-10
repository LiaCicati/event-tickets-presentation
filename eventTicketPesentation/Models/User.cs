using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace eventTicketPesentation.Models
{
    public class User
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [Required, EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [Required, MaxLength(20), MinLength(6)]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("admin")] public bool Admin { get; set; }
    }
}