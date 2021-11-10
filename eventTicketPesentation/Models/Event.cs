using System;
using System.ComponentModel.DataAnnotations;

namespace eventTicketPesentation.Models
{
    public class Event
    {
        public long id { get; set; }

        [Required(ErrorMessage = "Name Field is Required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Description Field is Required")]
        public string description { get; set; }

        [Required] public string location { get; set; }
        [Required] public string thumbnail { get; set; }
        [Required] public int nrOfTickets { get; set; }
        [Required] public string Category { get; set; }
        public bool isCancelled { get; set; }
        [Required] public DateTime dateTime { get; set; }
        public Address Address { get; set; }
        public double price { get; set; }

        public override string ToString()
        {
            return $"{id} {name} {description}";
        }
    }
}