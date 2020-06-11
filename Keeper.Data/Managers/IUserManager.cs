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
    }
}
