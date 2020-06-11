using Keeper.Data.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public class UserManager : IUserManager
    {
        private KeeperDbContext _dbContext;

        public UserManager(KeeperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> ActivateUserAsync(string token)
        {
            var user = await _dbContext.Users.FirstAsync(x => x.Token == token);
            if (user != null)
            {
                user.Active = true;
                user.Token = null;
                user.Modified = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return user;
            }

            return null;
        }

        public async Task<User> CreateUserAsync(string firstName, string lastName, string email, string password)
        {
            var user = await _dbContext.Users.FirstAsync(x => x.Email == email);
            if (user != null)
            {
                var salt = GenerateSalt();
                var newUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = HashPassword(password, salt),
                    Salt = Convert.ToBase64String(salt),
                    Active = false,
                    Token = Guid.NewGuid().ToString(),
                    Currency = "ZAR",
                    Created = DateTime.UtcNow
                };

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                return newUser;
            }

            return null;
        }

        public async Task<User> GetUserByLoginAsync(string email, string password)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstAsync(x => x.Email == email);
            
            // check if salt + password combination matches with
            // what's stored in db
            if (user != null) 
            {
                var byteSalt = Convert.FromBase64String(user.Salt);
                var hashed = HashPassword(password, byteSalt);
                if (user.Password == hashed)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<string> ResetPasswordAsync(string email)
        {
            var user = await _dbContext.Users.FirstAsync(x => x.Email == email);
            if (user != null)
            {
                var token = Guid.NewGuid().ToString();

                user.Password = null;
                user.Salt = null;
                user.Token = token;
                user.Modified = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return token;
            }

            return null;
        }

        public async Task<bool> UpdatePasswordAsync(string password, string token)
        {
            var user = await _dbContext.Users.FirstAsync(x => x.Token == token);
            if (user != null)
            {
                var salt = GenerateSalt();
                user.Salt = Convert.ToBase64String(salt);
                user.Password = HashPassword(password, salt);
                user.Token = null;
                user.Modified = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        #region Private methods

        private byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256/8));
        }

        #endregion
    }
}
