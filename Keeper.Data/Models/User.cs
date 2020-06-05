using System;
using System.Collections.Generic;

namespace Keeper.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public double HourlyRate { get; set; }
        public List<Client> Clients { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
