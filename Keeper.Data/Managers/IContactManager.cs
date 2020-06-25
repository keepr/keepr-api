using Keeper.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public interface IContactManager
    {
        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id">contact id</param>
        /// <param name="userId">user id</param>
        /// <returns>Contact</returns>
        public Task<Contact> GetByIdAsync(int id, int userId);

        /// <summary>
        /// Create a Contact
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="email">email address</param>
        /// <param name="phone">phone number</param>
        /// <param name="clientId">client id</param>
        /// <param name="userId">user id</param>
        /// <returns>Newly created Contact</returns>
        public Task<Contact> CreateAsync(string firstName, string lastName, string email, string phone, int clientId, int userId);

        /// <summary>
        /// Update a Contact
        /// </summary>
        /// <param name="id">contact id</param>
        /// <param name="firstName">new first name</param>
        /// <param name="lastName">new last name</param>
        /// <param name="email">new email address</param>
        /// <param name="phone">new phone number</param>
        /// <param name="userId">user id</param>
        /// <returns>Updated Contact</returns>
        public Task<Contact> UpdateAsync(int id, string firstName, string lastName, string email, string phone, bool primary, int userId);

        /// <summary>
        /// Delete a Contact
        /// </summary>
        /// <param name="id">contact id</param>
        /// <param name="userId">user id</param>
        /// <returns>true or false depending on success</returns>
        public Task<bool> DeleteAsync(int id, int userId);
    }
}
