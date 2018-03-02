using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.Exceptions;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Extensions;
using iShop.Service.Commons;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace iShop.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        

        public AccountService(
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper, ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IServiceResult> CreateAsync(RegisterDto dto)
        {
            try
            {
                var user = _mapper.Map<RegisterDto, ApplicationUser>(dto);

                // In this application, UserName is Email
                user.UserName = dto.Email;

                // Create a new User by User's info and password
                var createResult = await _userManager.CreateAsync(user, dto.Password);

                // Add roles to the User 
                var addRoleResult = await AddToRoleAsync(user, ApplicationConstants.RoleName.User);
                // Add claims as well
                var addClaimResult = await AddClaimAsync(user, ApplicationConstants.ClaimName.User, "true");

                // Everything is fine, return 200 and log 

                if (!createResult.Succeeded || !addRoleResult.Succeeded || !addClaimResult.Succeeded)
                {
                    throw new Exception(createResult.GetErrorDescription() + addRoleResult.GetErrorDescription() +
                                        addClaimResult.GetErrorDescription());
                }
                _logger.LogInformation($"User {user.UserName} with id: {user.Id} created.");
                var result = await GetSingleAsync(user.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Can not create user {dto.Email}. {e.Message}");
                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> GetSingleAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    throw new InvalidException("Username/Password");

                var userDto = _mapper.Map<ApplicationUser, ApplicationUserDto>(user);
                return new ServiceResult(payload: userDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Can not get user with id: {id}. {e.Message}");
                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> GetAllAsync(QueryObject queryTerm)
        {
            try
            {
                var users = await _userManager.GetUsersInRoleAsync(ApplicationConstants.RoleName.User);
                var usersDto = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserDto>>(users);

                return new ServiceResult(payload: usersDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Can not get all users. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> UpdateAsync(string id, RegisterDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    throw new InvalidException("Username/Password");
                _mapper.Map(dto, user);

                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Errors.Count() != 0)
                    return new ServiceResult(false, payload: updateResult.Errors);
                _logger.LogInformation($"User {user.UserName} updated.");
                var result = await GetSingleAsync(user.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Can not update user {dto.Email}. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    throw new InvalidException("Username/Password");
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    throw new Exception(result.GetErrorDescription());
                _logger.LogInformation($"Deleted user with id: {id}.");
                return new ServiceResult();
            }
            catch (Exception e)
            {
                _logger.LogError($"Can not delete user with id {id}.");
                return new ServiceResult(false, e.Message);
            }
        }

        private async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            try
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = new ApplicationRole {Name = roleName};
                    var createRoleResult = await _roleManager.CreateAsync(role);
                    if (!createRoleResult.Succeeded)
                        throw new Exception($"Can not create role: {roleName}. {createRoleResult.GetErrorDescription()}");
                }
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                    return result;
                throw new Exception(
                    $"Can not add {roleName} to user with id: {user.Id}. {result.GetErrorDescription()}");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        private async Task<IdentityResult> AddClaimAsync(ApplicationUser user, string claimType, string claimValue)
        {
            try
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var claim = userClaims.FirstOrDefault(u => u.Type == claimType);
                if (claim == null)
                {
                    var result = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
                    if (!result.Succeeded)
                        throw new Exception($"Can not add claim to user {user.Id}. {result.GetErrorDescription()}");
                    return result;
                }
                else
                {
                    var removeClaimResult = await _userManager.RemoveClaimAsync(user, claim);
                    if (!removeClaimResult.Succeeded)
                        throw new Exception(
                            $"Can not remove claim {claim.Type} {claim.Value} to user {user.Id}. {removeClaimResult.GetErrorDescription()}");
                    var result = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
                    if (!result.Succeeded)
                        throw new Exception($"Can not add claim to user {user.Id}. {result.GetErrorDescription()}");
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
         
        }
    }
}
