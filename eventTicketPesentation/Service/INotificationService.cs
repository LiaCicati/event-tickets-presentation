using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Service
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<List<Notification>> GetNotificationsByUserAsync(long id);
    }
}