using System;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.Exceptions;
using iShop.Data.Entities;
using iShop.Service.Commons;
using iShop.Service.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace iShop.Service.Implementations
{
    public class UserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountService> _logger;

        public UserManagerService(UserManager<ApplicationUser> userManager, ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IServiceResult> GetUserInfo(Guid userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                {
                    var errorMessage = "The user profile is not found or no longer available.";
                    throw new NotFoundException(errorMessage, null);
                }

                var userResource = Mapper.Map<ApplicationUser, ApplicationUserDto>(user);

                var roles = await _userManager.GetRolesAsync(user);

                var userData = new { userInfo = userResource, roles = roles };

                return new ServiceResult(payload: userData);
            }
            catch (Exception e)
            {
                _logger.LogError($"User with id {userId} failed to request to get the info. {e.Message}");
                return new ServiceResult(false, e.Message);
            }
        }
    }
}
