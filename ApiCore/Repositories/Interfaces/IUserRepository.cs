using ApiCore.Entities;
using ApiCore.Models;
using System.Threading.Tasks;

namespace ApiCore.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase
    {
        Task<bool> UserExists(string email);
        Task<User> Register(User user, string pas);
        Task<User> Login(LoginUserModel loginUserModel);
    }
}
