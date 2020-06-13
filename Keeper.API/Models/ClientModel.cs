using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
