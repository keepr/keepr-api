using Keeper.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public interface IClientManager
    {
        public Task<Client> GetClientByIdAsync(int id, int userId);
        public Task<IEnumerable<Client>> GetClientsByUserIdAsync(int userId);
        public Task<Client> CreateClientAsync(string name, string address, int userId);
        public Task<Client> UpdateClientAsync(int id, string name, string address, int userId);
        public Task<bool> DeleteClientAsync(int id, int userId);
    }
}
