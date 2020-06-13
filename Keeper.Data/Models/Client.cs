using System;
using System.Collections.Generic;

namespace Keeper.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        // FK
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
