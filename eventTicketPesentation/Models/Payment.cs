using System;
using System.Text.Json.Serialization;

namespace eventTicketPesentation.Models
{
    public class Payment
    {
        [JsonPropertyName("creditCardNumber")] public string CreditCardNumber { get; set; }
        [JsonPropertyName("dateTime")] public DateTime DateTime { get; set; }
        [JsonPropertyName("amount")] public double Amount { get; set; }
        [JsonPropertyName("buyer")] public long Buyer { get; set; }
        [JsonPropertyName("event")] public long Event { get; set; }
    }
}