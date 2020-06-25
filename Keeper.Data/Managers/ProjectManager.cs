using Keeper.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public class ProjectManager: IProjectManager
    {
        private KeeperDbContext _dbContext;

        public ProjectManager(KeeperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ToggleArchiveAsync(int id, int userId)
        {
            var project = await _dbContext.Projects
                .SingleOrDefaultAsync(x => x.Id == id && x.Client.UserId == userId);

            if (project != null)
            {
                project.Archive = !project.Archive;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Project> CreateAsync(string name, double? budget, string currency, double? hourlyRate, int clientId, int userId)
        {
            var client = await _dbContext.Clients
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == clientId && x.UserId == userId);

            if (client != null)
            {
                var project = new Project()
                {
                    Name = name,
                    Budget = budget ?? 0,
                    Currency = currency,
                    HourlyRate = hourlyRate ?? 0,
                    ClientId = clientId,
                    Created = DateTime.UtcNow
                };

                _dbContext.Projects.Add(project);
                await _dbContext.SaveChangesAsync();

                return project;
            }

            return null;
        }

        public async Task<Project> UpdateAsync(int id, string name, double? budget, string currency, double? hourlyRate, int userId)
        {
            var project = await _dbContext.Projects
                .SingleOrDefaultAsync(x => x.Id == id && x.Client.UserId == userId);

            if (project != null)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    project.Name = name;
                }

                if (budget.HasValue)
                {
                    project.Budget = budget.Value;
                }

                if (!string.IsNullOrEmpty(currency))
                {
                    project.Currency = currency;
                }

                if (hourlyRate.HasValue)
                {
                    project.HourlyRate = hourlyRate.Value;
                }


                project.Modified = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();

                return project;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var project = await _dbContext.Projects
                .SingleOrDefaultAsync(x => x.Id == id && x.Client.UserId == userId);

            if (project != null)
            {
                _dbContext.Projects.Remove(project);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Project> GetByIdAsync(int id, int userId)
        {
            return await _dbContext.Projects
                .AsNoTracking()
                .Include(x => x.Tasks)
                .SingleOrDefaultAsync(x => x.ClientId == id && x.Client.UserId == userId);
        }

        public async Task<IEnumerable<Project>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Projects
                .AsNoTracking()
                .Include(x => x.Tasks)
                .Where(x => x.Client.UserId == userId)
                .ToListAsync();
        }
    }
}
