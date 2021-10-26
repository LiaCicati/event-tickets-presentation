using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service
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

        public User Login(LoginUserDTO loginUserDto)
        {
            try
            {
                var msg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(loginUserDto));
                var result = sendRequest<User>("loginUser", msg);
                IList<User> claims = new List<User>();
                
            }
            catch (Exception e)
            {
                throw new ArgumentException("Failed to login");
            }
        }
    }
}