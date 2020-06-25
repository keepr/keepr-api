using System;
using System.Collections.Generic;
using System.Linq;
using Keeper.Data.Models;

namespace Keeper.API.Models
{
    public class ClientModel
    {
        public ClientModel() {}

        public ClientModel(Client client)
        {
            this.Id = client.Id;
            this.Name = client.Name;
            this.Address = client.Address;
            this.Created = client.Created;
            this.Modified = client.Modified;

            if (client.Contacts != null)
            {
                this.Contacts = client.Contacts.Select(x => new ContactModel(x));
            }

            if (client.Projects != null)
            {
                this.Projects = client.Projects.Select(x => new ProjectModel(x));
            }
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<ContactModel> Contacts { get; set; }
        public IEnumerable<ProjectModel> Projects { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
