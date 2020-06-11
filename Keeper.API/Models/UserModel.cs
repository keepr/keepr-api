using Keeper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.API.Models
{
    public class UserModel
    {
        public UserModel() {}

        public UserModel(User model)
        {
            this.Id = model.Id;
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.Email = model.Email;
            this.Address = model.Address;
            this.Currency = model.Currency;
            this.HourlyRate = model.HourlyRate;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public double HourlyRate { get; set; }
    }
}
