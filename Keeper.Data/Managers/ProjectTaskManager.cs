using Keeper.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public class ProjectTaskManager : IProjectTaskManager
    {

        private KeeperDbContext _dbContext;

        public ProjectTaskManager(KeeperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectTask> CreateAsync(string name, string description, double? hours, DateTimeOffset? date, int projectId, int userId)
        {
            var project = await _dbContext.Projects
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == projectId && x.Client.UserId == userId);

            if (project != null)
            {
                var newTask = new ProjectTask()
                {
                    Name = name,
                    Description = description,
                    Hours = hours ?? 0,
                    Date = date ?? DateTime.UtcNow,
                    Created = DateTime.UtcNow,
                    ProjectId = projectId
                };

                _dbContext.ProjectTasks.Add(newTask);
                await _dbContext.SaveChangesAsync();

                return newTask;
            }

            return null;
        }

        public async Task<ProjectTask> UpdateAsync(int id, string name, string description, double? hours, DateTimeOffset? date, int userId)
        {
            var task = await _dbContext.ProjectTasks
                .SingleOrDefaultAsync(x => x.Id == id && x.Project.Client.UserId == userId);

            if (task != null)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    task.Name = name;
                }

                if (!string.IsNullOrEmpty(description))
                {
                    task.Description = description;
                }

                if (hours.HasValue)
                {
                    task.Hours = hours.Value;
                }

                if (date.HasValue)
                {
                    task.Date = date.Value;
                }

                task.Modified = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();

                return task;
            }


            return null;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var task = await _dbContext.ProjectTasks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id && x.Project.Client.UserId == userId);

            if (task != null)
            {
                _dbContext.ProjectTasks.Remove(task);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<ProjectTask> GetByIdAsync(int id, int userId)
        {
            return await _dbContext.ProjectTasks
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id && x.Project.Client.UserId == userId);
        }
    }
}
