using System.Text.Json.Serialization;
namespace eventTicketPesentation.Service.dto
{
    public class LoginUserDTO
    {
       
       [ JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}