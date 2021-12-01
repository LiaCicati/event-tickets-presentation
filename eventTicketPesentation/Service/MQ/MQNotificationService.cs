using System.Collections.Generic;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using RabbitMQ.Client;

namespace eventTicketPesentation.Service.MQ
{
    public class MQNotificationService : MQService, INotificationService
    {
        public MQNotificationService(IModel channel) : base(channel)
        {
        }

        public Task<Notification> CreateNotificationAsync(Notification notification)
        {
            return SendAndConvertAsync<Notification, Notification>("createNotification", notification);
        }

        public Task<List<Notification>> GetNotificationsByUserAsync(long id)
        {
            return SendAndConvertAsync<List<Notification>,long>("getNotificationsByUser", id);
        }
    }
}