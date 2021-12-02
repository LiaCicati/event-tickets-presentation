using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace eventTicketPesentation.Service
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _claims = new ClaimsPrincipal();
        public User LoggedInUser { get; set; }
        private readonly IJSRuntime jsRuntime;
        private IUserService _userService;
        private User cachedUser;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService)
        {
            this.jsRuntime = jsRuntime;
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
            cachedUser = null;
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            LoggedInUser = null;
            Claims = new ClaimsPrincipal();
        }

        public User GetCachedUser()
        {
            return cachedUser;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (cachedUser == null)
            {
                string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    User tmp = JsonSerializer.Deserialize<User>(userAsJson);
                }
            }

            return await Task.FromResult(new AuthenticationState(Claims));
        }
    }
}