using System.Threading.Tasks;
using eventTicketPesentation.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace eventTicketPesentation.Service
{
    public class CustomAuthenticationStateProvider:AuthenticationStateProvider
    {
        public User LoggedInUser { get; set; }
        
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
        }
    }
}