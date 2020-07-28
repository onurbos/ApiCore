using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ApiCore.Repositories.Interfaces;
using ApiCore.Helpers;

namespace ApiCore.Entities
{
    [Route("api/auth"), AllowAnonymous, ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(IConfiguration configuration, IUserRepository userRepository, UserManager<IdentityUser> userManager, IUserHelper userHelper, SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _userManager = userManager;
            _userHelper = userHelper;
            _signInManager = signInManager;
        }

        [Route("login"), HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserModel loginUserModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var signInResult = await _signInManager.PasswordSignInAsync(loginUserModel.Email.Trim(), loginUserModel.Password, false, false);


            if (!signInResult.Succeeded)
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(loginUserModel.Email);

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, loginUserModel.Email),
                    new Claim("user_id", user.Id.ToString()),
                    new Claim("user_name", user.UserName),
                    new Claim("email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256)
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserModel userModel)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _userRepository.UserExists(userModel.Email))
                ModelState.AddModelError("Email", "Email already exist.");

            var test = new AddUserModel
            {
                Email = userModel.Email,
                LastName = userModel.LastName,
                FirstName = userModel.FirstName,
                Password = userModel.Password
            };

            var identityUser = await _userHelper.CreateIdentityUser(test);

            if (identityUser == null)
                return BadRequest(ModelState);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            var userToCreate = UserHelper.UserEntity(userModel);

            var currentUser = new CurrentUser { UserId = userToCreate.Id };

            await _userRepository.Register(userToCreate, userModel.Password);

            return Ok(userModel);
        }
    }
}
