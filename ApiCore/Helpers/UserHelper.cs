using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ApiCore.Entities;
using ApiCore.Models;
using IdentityModel;

namespace ApiCore.Helpers
{
    public interface IUserHelper
    {
        Task<IdentityUser> CreateIdentityUser(AddUserModel userModel);
    }

    public class UserHelper : IUserHelper
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public IConfiguration _configuration { get; }


        public UserHelper(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        public static User UserEntity(Models.AddUserModel userModel)
        {
            var user = new User
            {
                Id = 1,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                FullName = userModel.FullName,
                Deleted = false,

            };
            return user;
        }

        public async Task<IdentityUser> CreateIdentityUser(AddUserModel userModel)
        {
            if (await IsUserExist(userModel.Email))
                return null;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var user = new IdentityUser
                {
                    UserName = userModel.Email,
                    Email = userModel.Email,
                    NormalizedEmail = userModel.Email,
                    NormalizedUserName = !string.IsNullOrEmpty(userModel.FirstName) ? userModel.FirstName + " " + userModel.LastName : ""
                };

                var result = await _userManager.CreateAsync(user, userModel.Password);

                if (!result.Succeeded)
                    return null;


                await _userManager.AddClaimsAsync(user, new[]
                {
                    new Claim(JwtClaimTypes.Name, !string.IsNullOrEmpty(userModel.FirstName) ? userModel.FirstName + " " + userModel.LastName : ""),
                    new Claim(JwtClaimTypes.GivenName, userModel.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, userModel.LastName),
                    new Claim(JwtClaimTypes.Email, userModel.Email),
                    new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                });

                user = await _userManager.FindByNameAsync(userModel.Email);

                return user;
            }
        }

        public async Task<bool> IsUserExist(string email)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                return await userManager.FindByNameAsync(email) != null;
            }
        }
    }
}
