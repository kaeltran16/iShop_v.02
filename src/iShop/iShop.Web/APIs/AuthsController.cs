using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This Controller is reponsible for creating and validating JWT
    /// </summary>
    [Route("api/[controller]")]
    public class AuthsController : Controller
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginDto dto)
        {
            var result = await _authService.Login(dto.Username, dto.Password);

            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());
        }
    }
}
