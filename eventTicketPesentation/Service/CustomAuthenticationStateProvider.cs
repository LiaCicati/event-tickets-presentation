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


            LoggedInUser = result;
            SetUpClaims(result);
            await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser",
                JsonSerializer.Serialize(LoggedInUser));
        }

        public void SetUpClaims(User user)
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("id", "" + user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.FullName));
            claims.Add(new Claim("isAdmin", user.Admin.ToString()));
            var claimIdentity = new ClaimsIdentity(claims, "apiauth_type");
            Claims = new ClaimsPrincipal(claimIdentity);
        }

        public void Logout()
        {
            LoggedInUser = null;
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            Claims = new ClaimsPrincipal();
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (LoggedInUser == null)
            {
                string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    LoggedInUser = JsonSerializer.Deserialize<User>(userAsJson);
                    SetUpClaims(LoggedInUser);
                }
            }

            return await Task.FromResult(new AuthenticationState(Claims));
        }
    }
}