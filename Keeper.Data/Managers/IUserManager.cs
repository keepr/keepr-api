using Keeper.Data.Models;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public interface IUserManager
    {
        /// <summary>
        /// Get User account by email and password
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="password">password</param>
        /// <returns>User</returns>
        public Task<User> GetByLoginAsync(string email, string password);

        /// <summary>
        /// Create a User account
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="email">email address</param>
        /// <param name="password">password</param>
        /// <returns>Newly created User</returns>
        public Task<User> CreateAsync(string firstName, string lastName, string email, string password);

        /// <summary>
        /// Activate a User account
        /// </summary>
        /// <param name="token">activation token</param>
        /// <returns>Updated User</returns>
        public Task<User> ActivateAsync(string token);

        /// <summary>
        /// Reset the password of a User account
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>Password Reset token</returns>
        public Task<string> ResetPasswordAsync(string email);

        /// <summary>
        /// Set / Update password
        /// </summary>
        /// <param name="password">new password</param>
        /// <param name="token">Password reset token from ResetPasswordAsync</param>
        /// <returns>true or false depending on success</returns>
        public Task<bool> UpdatePasswordAsync(string password, string token);

        /// <summary>
        /// Update a User account
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="firstName">new first name</param>
        /// <param name="lastName">new last name</param>
        /// <param name="email">new email address</param>
        /// <param name="address">new address</param>
        /// <param name="password">new password</param>
        /// <param name="currency">new currency</param>
        /// <param name="hourlyRate">new hourly rate</param>
        /// <returns>Updated User account</returns>
        public Task<User> UpdateAsync(int id, string firstName, string lastName, string email, string address,
            string password, string currency, double? hourlyRate);

        /// <summary>
        /// Get User account by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>User</returns>
        public Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Delete User account by Id
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>true or false depending on success</returns>
        public Task<bool> DeleteByIdAsync(int id);
    }
}
