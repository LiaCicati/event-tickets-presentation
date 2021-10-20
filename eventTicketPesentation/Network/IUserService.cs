using eventTicketPesentation.Models;

namespace eventTicketPesentation.Network
{
    public interface IUserService
    {
        User RegisterUser(User user);
    }
}