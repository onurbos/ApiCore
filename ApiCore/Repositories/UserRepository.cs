using ApiCore.Context;
using ApiCore.Entities;
using ApiCore.Models;
using ApiCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ApiCore.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(AppDBContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
        {
        }

        public async Task<User> Login(LoginUserModel loginUserModel)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Email == loginUserModel.Email);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(loginUserModel.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }


        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await DbContext.Users.AnyAsync(x => x.Email == email))
                return true;

            return false;
        }
    }
}
