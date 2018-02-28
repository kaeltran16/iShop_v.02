//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using AutoMapper;
//using iShop.Common.DTOs;
//using iShop.Common.Exceptions;
//using iShop.Common.Helpers;
//using iShop.Data.Entities;
//using iShop.Repo.Extensions;
//using iShop.Service.Commons;
//using iShop.Service.Interfaces;
//using Microsoft.AspNetCore.Identity;

//namespace iShop.Service.Implementations
//{
//    public class AccountService: IAccountService
//    {
//         private readonly UserManager<ApplicationUser> _userManager;
//        //private readonly ILogger<AccountsController> _logger;
//        private readonly IMapper _mapper;

//        public AccountService(
//            UserManager<ApplicationUser> userManager,
//            //ILogger<AccountsController> logger
//                IMapper mapper)
//        {
//            _userManager = userManager;
//            //_logger = logger;
//            _mapper = mapper;
//        }

//        public async Task<IServiceResult> CreateAsync(RegisterDto dto)
//        {
//            try
//            {
//                var user = _mapper.Map<RegisterDto, ApplicationUser>(dto);

//                // In this application, UserName is Email
//                user.UserName = dto.Email;

//                // Create a new User by User's info and password
//                var createResult = await _userManager.CreateAsync(user, dto.Password);

//                // Add roles to the User 

//                // Add claims as well


//                // Everything is fine, return 200 and log 

//                if (!createResult.Succeeded)
//                {
//                    throw new Exception(createResult.GetErrorDescription());
//                    //_logger.LogInformation(LoggingEvents.Success, "User with email " + model.Email + " created");
//                }
//                var result = await GetSingleAsync(user.Id.ToString());
//                return new ServiceResult(payload: result.Payload);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }
//        }

//        public async Task<IServiceResult> GetSingleAsync(string id)
//        {
//            try
//            {
//                var user = await _userManager.FindByIdAsync(id);
//                if (user == null)
//                    throw new InvalidException("Username/Password");

//                var userDto = _mapper.Map<ApplicationUser, ApplicationUserDto>(user);
//                return new ServiceResult(payload: userDto);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }
//        }

//        public async Task<IServiceResult> GetAllAsync()
//        {
//            try
//            {
//                var users = await _userManager.GetUsersInRoleAsync(ApplicationConstants.RoleName.User);
//                var usersDto = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserDto>>(users);

//                return new ServiceResult(payload: usersDto);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }
//        }

//        public async Task<IServiceResult> UpdateAsync(string id, RegisterDto dto)
//        {   
//            try
//            {
//                var user = await _userManager.FindByIdAsync(id);
//                if (user == null)
//                    throw new InvalidException("Username/Password");
//                _mapper.Map(dto, user);

//                var updateResult = await _userManager.UpdateAsync(user);
//                if (updateResult.Errors.Count() != 0)
//                    return new ServiceResult(false, payload: updateResult.Errors);

//                var result = await GetSingleAsync(user.Id.ToString());
//                return new ServiceResult(payload: result.Payload);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }
//        }

//        public async Task<IServiceResult> RemoveAsync(string id)
//        {
//            try
//            {
//                var user = await _userManager.FindByIdAsync(id);
//                if (user == null)
//                    throw new InvalidException("Username/Password");
//                var result = await _userManager.DeleteAsync(user);
//                if (!result.Succeeded)
//                    throw new Exception(result.GetErrorDescription());

//                return new ServiceResult();
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }    
//        }

//        private async Task<IdentityResult> AddToRoleAsync(ApplicationUser user)
//        {
//            var result = await _userManager.AddToRoleAsync(user, ApplicationConstants.RoleName.User);
//            return result;
//        }

//        private async Task<IdentityResult> AddClaimsAsync(ApplicationUser user)
//        {
//            var result = await _userManager.AddClaimAsync(user, new Claim(ApplicationConstants.ClaimName.User, "true"));
//            return result;
//        }
//    }
//}
