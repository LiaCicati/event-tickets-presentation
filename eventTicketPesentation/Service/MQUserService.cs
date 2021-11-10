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
        private CustomAuthenticationStateProvider _authenticationStateProvider;

        public MQUserService(IModel channel, AuthenticationStateProvider authenticationStateProvider) :
            base(channel)
        {
            this._authenticationStateProvider = (CustomAuthenticationStateProvider) authenticationStateProvider;
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
                var result = await SendAndConvertAsync<User, LoginUserDTO>("loginUser", loginUserDto);

                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim("email", result.Email));
                claims.Add(new Claim("id", "" + result.Id));
                claims.Add(new Claim(ClaimTypes.Name, result.FullName));
                claims.Add(new Claim("isAdmin", result.Admin.ToString()));
                var claimIdentity = new ClaimsIdentity(claims, "apiauth_type");
                var principal = new ClaimsPrincipal(claimIdentity);
                
                _authenticationStateProvider.Claims = principal;
                _authenticationStateProvider.LoggedInUser = result;
                
                return result;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Failed to login");
            }
        }

        public void Logout()
        {
            _authenticationStateProvider.Claims = new ClaimsPrincipal();
        }

        public Task<User> UpdateUserAsync(User user)
        {
            return SendAndConvertAsync<User, User>("updateUser", user);
        }
    }
}