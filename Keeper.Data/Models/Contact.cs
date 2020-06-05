using System;

namespace Keeper.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        // FK
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
