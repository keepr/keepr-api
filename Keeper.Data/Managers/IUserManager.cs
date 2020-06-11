using Keeper.Data.Models;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public interface IUserManager
    {
        public Task<User> GetUserByLoginAsync(string email, string password);
        public Task<User> CreateUserAsync(string firstName, string lastName, string email, string password);
        public Task<User> ActivateUserAsync(string token);
        public Task<string> ResetPasswordAsync(string email);
        public Task<bool> UpdatePasswordAsync(string password, string token);
        public Task<User> UpdateUserAsync(int id, string firstName, string lastName, string email, string address,
            string password, string currency, double? hourlyRate);

        public Task<User> GetUserByIdAsync(int id);
    }
}
