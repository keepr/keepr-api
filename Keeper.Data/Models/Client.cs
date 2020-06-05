using System;
using System.Collections.Generic;

namespace Keeper.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Project> Projects { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        // FK
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
