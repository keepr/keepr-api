using Keeper.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public interface IProjectManager
    {
        /// <summary>
        /// Get Project by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Project</returns>
        public Task<Project> GetByIdAsync(int id);

        /// <summary>
        /// Get Projects by Client Id
        /// </summary>
        /// <param name="clientId">client id</param>
        /// <returns>List of Projects</returns>
        public Task<IEnumerable<Project>> GetByClientIdAsync(int clientId);

        /// <summary>
        /// Get Projects by User Id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>List of Projects</returns>
        public Task<IEnumerable<Project>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Create a Project
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="budget">budget</param>
        /// <param name="currency">currency</param>
        /// <param name="hourlyRate">hourly rate</param>
        /// <param name="clientId">client id</param>
        /// <returns>Newly created Project</returns>
        public Task<Project> CreateAsync(string name, double budget, string currency, double hourlyRate, int clientId);

        /// <summary>
        /// Update a Project
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">new name</param>
        /// <param name="budget">new budget</param>
        /// <param name="currency">new currency</param>
        /// <param name="hourlyRate">new hourly rate</param>
        /// <returns>Updated Project</returns>
        public Task<Project> UpdateAsync(int id, string name, double budget, string currency, double hourlyRate);

        /// <summary>
        /// Archive a Project
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>true or false depending on success</returns>
        public Task<bool> ArchiveAsync(int id);

        /// <summary>
        /// Delete a Project
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>true or false depending on success</returns>
        public Task<bool> DeleteAsync(int id);
    }
}
