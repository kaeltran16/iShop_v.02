using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.Exceptions;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Service.Commons;
using iShop.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iShop.Service.Implementations
{
    public class AuthService : IAuthService
    {   
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthService> _logger;
        private readonly JwtTokenSettings _tokenSettings;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthService> logger, IOptionsSnapshot<JwtTokenSettings> tokenSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _tokenSettings = tokenSettings.Value;
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
                var token = await GenerateToken(user);
                _logger.LogInformation($"User with username {username} just created a new token.");
                return new ServiceResult(payload: token);
            }
            catch (Exception e)
            {
                _logger.LogError($"User with username {username} failed to login.", e.Message);

                return new ServiceResult(false, e.Message);
            }
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            try
            {
                var jwt = new JwtSecurityToken(
                    issuer: _tokenSettings.Issuer,
                    audience: _tokenSettings.Audience,
                    claims: await CreateClaims(user),
                    notBefore: _tokenSettings.NotBefore,
                    expires: _tokenSettings.Expiration,
                    signingCredentials: _tokenSettings.SigningCredentials
                    );

                var encodedToken = new JwtSecurityTokenHandler().WriteToken(jwt);
                return encodedToken;
            }
            catch (Exception e)
            {
                _logger.LogError($"User with username {user.UserName} failed to create a new token. {e.Message}");
                throw new Exception($"Some errors occured. Can not create new JWT. The error is {e.Message}.");
            }

        }

        private async Task<Claim[]> CreateClaims(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenSettings.JtiGenerator()), 
                new Claim(JwtRegisteredClaimNames.Iat, 
                    _tokenSettings.IssuedAt.ToShortDateString() 
                    + _tokenSettings.IssuedAt.ToShortTimeString()),            
            };
            return claims;
        }
    }
}
