using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;
using Microsoft.AspNetCore.Components.Authorization;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service
{
    public class MQUserService : MQService, IUserService
    {
        private CustomAuthenticationStateProvider _authenticationStateProvider;

        public MQUserService(IModel channel, AuthenticationStateProvider authenticationStateProvider) :
            base(channel)
        {
            this._authenticationStateProvider = (CustomAuthenticationStateProvider) authenticationStateProvider;
        }

        public User RegisterUser(User user)
        {
            try
            {
                var msg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));
                return sendRequest<User>("registerUser", msg);  
            }
            catch (Exception e)
            {
                throw new ArgumentException("Same email provided");
            }
            
        }

        public User Login(LoginUserDTO loginUserDto)
        {
            try
            {
                var msg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(loginUserDto));
                var result = sendRequest<User>("loginUser", msg);
                Console.WriteLine();
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim("email", result.email));
                claims.Add(new Claim("id", "" + result.id));
                claims.Add(new Claim(ClaimTypes.Name, result.fullName));
                claims.Add(new Claim("isAdmin", result.admin.ToString()));
                Console.WriteLine("is admin? " + result.admin.ToString());
                var claimIdentity = new ClaimsIdentity(claims, "apiauth_type");
                var principal = new ClaimsPrincipal(claimIdentity);
                _authenticationStateProvider.LoggedInUser = principal;
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Failed to login");
            }
        }

        public void Logout()
        {
            _authenticationStateProvider.LoggedInUser = new ClaimsPrincipal();
        }

        public User UpdateUser(User user)
        {
            var msg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(user));
            return sendRequest<User>("updateUser", msg);
        }
    }
}