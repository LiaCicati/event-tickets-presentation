using System.Text;
using System.Text.Json;
using eventTicketPesentation.Models;
using RabbitMQ.Client;

namespace eventTicketPesentation.Network
{
    public class MQUserService : MQService, IUserService
    {
        public MQUserService(IModel channel) : base(channel)
        {
        }

        public User RegisterUser(User user)
        {
            var msg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));
            return sendRequest<User>("registerUser", msg);
        }
    }
}