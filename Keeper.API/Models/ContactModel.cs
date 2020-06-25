using System;
using Keeper.Data.Models;

namespace Keeper.API.Models
{
    public class ContactModel
    {
        public ContactModel() { }

        public ContactModel(Contact contact)
        {
            this.Id = contact.Id;
            this.FirstName = contact.FirstName;
            this.LastName = contact.LastName;
            this.Email = contact.Email;
            this.Phone = contact.Phone;
            this.Primary = contact.Primary;
            this.ClientId = contact.ClientId;
            this.Created = contact.Created;
            this.Modified = contact.Modified;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Primary { get; set; }
        public int ClientId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
