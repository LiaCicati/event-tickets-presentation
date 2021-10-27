using eventTicketPesentation.Models;
using eventTicketPesentation.Service.dto;

namespace eventTicketPesentation.Service
{
    public interface IUserService
    {
        User RegisterUser(User user);
        User Login(LoginUserDTO loginUserDto);
        void Logout();
        User UpdateUser(User user);
    }
}