using System;
using Keeper.Data.Models;

namespace Keeper.API.Models
{
    public class UserModel
    {
        public UserModel() {}

        public UserModel(User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Address = user.Address;
            this.Currency = user.Currency;
            this.HourlyRate = user.HourlyRate;
            this.Created = user.Created;
            this.Modified = user.Modified;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public double HourlyRate { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
