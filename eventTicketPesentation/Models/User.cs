using System;
using System.ComponentModel.DataAnnotations;

namespace eventTicketPesentation.Models
{
    public class User
    {
        public long id { get; set; }
        [Required, EmailAddress]
        public string email { get; set; }
        [Required]
        public string fullName { get; set; }
        [Required, MaxLength(20), MinLength(6) ]
        public string password { get; set; }
        public bool admin { get; set; }
    }
}