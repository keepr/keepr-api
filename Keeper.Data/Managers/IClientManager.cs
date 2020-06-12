using Keeper.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public interface IClientManager
    {
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userId">user id to prevent user accounts fetching other clients</param>
        /// <returns>Client</returns>
        public Task<Client> GetByIdAsync(int id, int userId);

        /// <summary>
        /// Get Clients for a User account
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List of Clients</returns>
        public Task<IEnumerable<Client>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Create a Client
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        /// <param name="userId">user id</param>
        /// <returns>Newly created Client</returns>
        public Task<Client> CreateAsync(string name, string address, int userId);

        /// <summary>
        /// Update a Client
        /// </summary>
        /// <param name="id">client id</param>
        /// <param name="name">new name</param>
        /// <param name="address">new address</param>
        /// <param name="userId">user id</param>
        /// <returns>Updated Client</returns>
        public Task<Client> UpdateAsync(int id, string name, string address, int userId);

        /// <summary>
        /// Delete a client
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userId">user id</param>
        /// <returns>true or false depending on success</returns>
        public Task<bool> DeleteAsync(int id, int userId);
    }
}
