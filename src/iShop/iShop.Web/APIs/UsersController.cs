//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace iShop.Infras.API.APIs
//{
//    public class UsersController: BaseController
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
 
//        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
//        {
//            _userManager = userManager;
//        }
 
//        //
//        // GET:
//        [Authorize]
//        [HttpGet("/api/userinfo")]
//        public async Task<IActionResult> Userinfo()
//        {
//            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());

//            if (user == null)
//            {
//                return BadRequest(new OpenIdConnectResponse
//                {
//                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
//                    ErrorDescription = "The user profile is no longer available."
//                });
//            }

//            var userResource = Mapper.Map<ApplicationUser, ApplicationUserDto>(user);

//            var roles = await _userManager.GetRolesAsync(user);

//            var userData = new {userInfo = userResource, roles = roles};
          

//            return Ok(userData);
//        }


//    }
//}
