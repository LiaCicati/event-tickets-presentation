using eventTicketPesentation.Models;
using eventTicketPesentation.Network.dto;

namespace eventTicketPesentation.Network
{
    public interface IUserService
    {
        User RegisterUser(User user);
        User Login(LoginUserDTO loginUserDto);
    }
}