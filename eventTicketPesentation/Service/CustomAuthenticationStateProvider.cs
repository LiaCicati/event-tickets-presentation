using System;
using System.Security.Claims;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace eventTicketPesentation.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _loggedInUser = new ClaimsPrincipal();

        public ClaimsPrincipal LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(value)));
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(new AuthenticationState(LoggedInUser));
        }
    }
}