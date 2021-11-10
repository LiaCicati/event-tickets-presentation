using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;
using Microsoft.AspNetCore.Components.Authorization;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service
{
    public class MQUserService : MQService, IUserService
    {
      

        public MQUserService(IModel channel) :
            base(channel)
        {
           
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            try
            {
                return await SendAndConvertAsync<User, User>("registerUser", user);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Same email provided");
            }
        }

        public async Task<User> LoginAsync(LoginUserDTO loginUserDto)
        {
            try
            {
                return await SendAndConvertAsync<User, LoginUserDTO>("loginUser", loginUserDto);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Failed to login");
            }
        }

        public Task<User> UpdateUserAsync(User user)
        {
            return SendAndConvertAsync<User, User>("updateUser", user);
        }
    }
}