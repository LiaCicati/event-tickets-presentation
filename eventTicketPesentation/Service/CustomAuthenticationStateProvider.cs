using System;
using System.Security.Claims;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace eventTicketPesentation.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _claims = new ClaimsPrincipal();
        public User LoggedInUser { get; set; }
      

        public ClaimsPrincipal Claims
        {
            get => _claims;
            set
            {
                _claims = value;
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(value)));
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(new AuthenticationState(Claims));
        }
    }
}