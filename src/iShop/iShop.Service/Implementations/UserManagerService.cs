//using System;
//using System.Threading.Tasks;
//using AutoMapper;
//using iShop.Common.DTOs;
//using iShop.Common.Exceptions;
//using iShop.Data.Entities;
//using iShop.Service.Commons;
//using Microsoft.AspNetCore.Identity;

//namespace iShop.Service.Implementations
//{
//    public class UserManagerService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;

//        public UserManagerService(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }

//        public async Task<IServiceResult> GetUserInfo(Guid userId)
//        {
//            try
//            {
//                var user = await _userManager.FindByIdAsync(userId.ToString());

//                if (user == null)
//                {
//                    var errorMessage = "The user profile is not found or no longer available.";
//                    throw new NotFoundException(errorMessage, null);
//                }

//                var userResource = Mapper.Map<ApplicationUser, ApplicationUserDto>(user);

//                var roles = await _userManager.GetRolesAsync(user);

//                var userData = new { userInfo = userResource, roles = roles };

//                return new ServiceResult(payload: userData);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }
//        }
//    }
//}
