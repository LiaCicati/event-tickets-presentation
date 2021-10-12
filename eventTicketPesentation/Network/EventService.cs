using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using eventTicketPesentation.Models;

namespace eventTicketPesentation.Network
{
    public class EventService
    {
        private HttpClient _client;

        public EventService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            var response = await _client.GetAsync("http://localhost:8080/events");
            var eventsJson = await response.Content.ReadAsStringAsync();

            var events = JsonSerializer.Deserialize<List<Event>>(eventsJson);

            return events;
        }
    }
}