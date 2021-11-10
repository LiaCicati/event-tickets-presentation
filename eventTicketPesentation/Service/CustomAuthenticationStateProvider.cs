using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;
using Microsoft.AspNetCore.Components.Authorization;

namespace eventTicketPesentation.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _claims = new ClaimsPrincipal();
        public User LoggedInUser { get; set; }
        private IUserService _userService;

        public CustomAuthenticationStateProvider(IUserService userService)
        {
            this._userService = userService;
        }


        public ClaimsPrincipal Claims
        {
            get => _claims;
            set
            {
                _claims = value;
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(value)));
            }
        }

        public async Task Login(LoginUserDTO loginUserDto)
        {
            var result = await _userService.LoginAsync(loginUserDto);
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim("email", result.Email));
            claims.Add(new Claim("id", "" + result.Id));
            claims.Add(new Claim(ClaimTypes.Name, result.FullName));
            claims.Add(new Claim("isAdmin", result.Admin.ToString()));
            var claimIdentity = new ClaimsIdentity(claims, "apiauth_type");
            var principal = new ClaimsPrincipal(claimIdentity);

            LoggedInUser = result;
            Claims = principal;
        }

        public void Logout()
        {
            LoggedInUser = null;
            Claims = new ClaimsPrincipal();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(new AuthenticationState(Claims));
        }
    }
}