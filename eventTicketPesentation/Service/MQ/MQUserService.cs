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

namespace eventTicketPesentation.Service.MQ
{
    public class MQUserService : MQService, IUserService
    {
        public MQUserService(IModel channel) :
            base(channel)
        {
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            return await SendAndConvertAsync<User, User>("registerUser", user);
        }

        public async Task<User> LoginAsync(LoginUserDTO loginUserDto)
        {
            return await SendAndConvertAsync<User, LoginUserDTO>("loginUser", loginUserDto);
        }

        public Task<User> UpdateUserAsync(User user)
        {
            return SendAndConvertAsync<User, User>("updateUser", user);
        }

        public Task<User> GrantAdminPrivilege(long userId)
        {
            return SendAndConvertAsync<User, long>("grantAdminPrivilege", userId);
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return SendAndConvertAsync<List<User>>("getAllUsers");
        }
    }
}