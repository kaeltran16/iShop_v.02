using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.Exceptions;
using iShop.Data.Entities;
using iShop.Service.Commons;
using iShop.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace iShop.Service.Implementations
{
    public class AuthService : IAuthService
    {   
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public async Task<IServiceResult> Login(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(username);
                if (user == null)
                    throw new InvalidException("Username/Password");
                var validationResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (!validationResult.Succeeded)
                    throw new InvalidException("Username/Password");
                var token = GenerateToken(user);
                _logger.LogInformation($"User with username {username} just created a new token.");
                return new ServiceResult(payload: token);
            }
            catch (Exception e)
            {
                _logger.LogError($"User with username {username} failed to login.", e.Message);

                return new ServiceResult(false, e.Message);
            }
        }

        private string GenerateToken(ApplicationUser user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("This is a secret key");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                       new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                       new Claim(ClaimTypes.Name, user.UserName)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                _logger.LogError($"User with username {user.UserName} failed to create a new token. {e.Message}");
                throw new Exception($"Some errors occured. Can not create new JWT. The error is {e.Message}.");
            }

        }
    }
}
