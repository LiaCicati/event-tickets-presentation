using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using eventTicketPesentation.Models;
using RabbitMQ.Client;

namespace eventTicketPesentation.Network
{
    public class EventService: IEventService
    {
        private HttpClient _client;

        public EventService(HttpClient client)
        {
            this._client = client;
        }

        public List<Event> GetAllEvents()
        {
            var response = _client.GetAsync("http://localhost:8080/events").Result;
            var eventsJson = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<List<Event>>(eventsJson);
        }
    }
}