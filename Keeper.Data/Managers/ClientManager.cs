using Keeper.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public class ClientManager : IClientManager
    {
        private KeeperDbContext _dbContext;

        public ClientManager(KeeperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> CreateAsync(string name, string address, int userId)
        {
            var newClient = new Client()
            {
                Name = name,
                Address = address,
                UserId = userId,
                Created = DateTime.UtcNow
            };

            _dbContext.Clients.Add(newClient);
            await _dbContext.SaveChangesAsync();

            return newClient;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var client = await _dbContext.Clients
                .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Client> GetByIdAsync(int id, int userId)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .Include(x => x.Contacts)
                .Include(x => x.Projects)
                .SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task<IEnumerable<Client>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .Include(x => x.Contacts)
                .Include(x => x.Projects)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Client> UpdateAsync(int id, string name, string address, int userId)
        {
            var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (client != null)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    client.Name = name;
                }

                if (!string.IsNullOrEmpty(address))
                {
                    client.Address = address;
                }

                client.Modified = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }

            return client;
        }
    }
}
