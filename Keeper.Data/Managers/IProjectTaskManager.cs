using Keeper.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public interface IProjectTaskManager
    {
        /// <summary>
        /// Get Project Task by Id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userId">user id</param>
        /// <returns>ProjectTask</returns>
        public Task<ProjectTask> GetByIdAsync(int id, int userId);

        /// <summary>
        /// Get Project Tasks by Project id
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="userId">user id</param>
        /// <returns>List of ProjectTasks</returns>
        public Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId, int userId);

        /// <summary>
        /// Create Project Task
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="description">description</param>
        /// <param name="hours">total hours spent</param>
        /// <param name="date">date on which it was done</param>
        /// <param name="projectId">project id</param>
        /// <param name="userId">user id</param>
        /// <returns>Newly created Project Task</returns>
        public Task<ProjectTask> CreateAsync(string name, string description, double? hours, DateTime? date, int projectId, int userId);

        /// <summary>
        /// Update Project Task
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">new name</param>
        /// <param name="description">new description</param>
        /// <param name="hours">new hours</param>
        /// <param name="date">date on which it was done</param>
        /// <param name="userId">user id</param>
        /// <returns>Updated Project Task</returns>
        public Task<ProjectTask> UpdateAsync(int id, string name, string description, double? hours, DateTime? date, int userId);

        /// <summary>
        /// Delete Project Task
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>true or false depending on success</returns>
        public Task<bool> DeleteAsync(int id, int userId);
    }
}
