using System;

namespace eventTicketPesentation.Models
{
    public class Event
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string thumbnail { get; set; }
        public int nrOfTickets { get; set; }
        public string Category { get; set; }
        public bool isCancelled { get; set; }
        public DateTime dateTime { get; set; }
    
        public override string ToString() {
            return $"{id} {name} {description}";
        }
    }
}