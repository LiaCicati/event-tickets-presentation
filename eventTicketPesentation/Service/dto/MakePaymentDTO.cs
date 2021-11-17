using System.Text.Json.Serialization;

namespace eventTicketPesentation.Service.dto
{
    public class MakePaymentDTO
    {
        [JsonPropertyName("creditCardNumber")] public string CreditCardNumber { get; set; }
        [JsonPropertyName("buyer")] public long Buyer { get; set; }
        [JsonPropertyName("event")] public long Event { get; set; }
    }
}