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
        /// <returns>Contact</returns>
        public Task<Contact> GetByIdAsync(int id);

        /// <summary>
        /// Get Contacts by Client Id
        /// </summary>
        /// <param name="clientId">client id</param>
        /// <returns>List of Contacts</returns>
        public Task<IEnumerable<Contact>> GetByClientIdAsync(int clientId);

        /// <summary>
        /// Create a Contact
        /// </summary>
        /// <param name="clientId">client id</param>
        /// <param name="firstName">first name</param>
        /// <param name="lastName">last name</param>
        /// <param name="email">email address</param>
        /// <param name="phone">phone number</param>
        /// <returns>Newly created Contact</returns>
        public Task<Contact> CreateAsync(int clientId, string firstName, string lastName, string email, string phone);

        /// <summary>
        /// Update a Contact
        /// </summary>
        /// <param name="id">contact id</param>
        /// <param name="firstName">new first name</param>
        /// <param name="lastName">new last name</param>
        /// <param name="email">new email address</param>
        /// <param name="phone">new phone number</param>
        /// <returns>Updated Contact</returns>
        public Task<Contact> UpdateAsync(int id, string firstName, string lastName, string email, string phone);
    }
}
