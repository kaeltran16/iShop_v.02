using System.Threading.Tasks;
using iShop.Common.Helpers;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This Controller is reponsible for creating and validating JWT
    /// </summary>
    [Route("api/[controller]")]
    public class AuthsController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthsController> _logger;

        public AuthsController(IAuthService authService, ILogger<AuthsController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginDto dto)
        {
            var result = await _authService.Login(dto.Username, dto.Password);

            var token = new {Token = result.Payload};
            if (result.IsSuccess)
                return Ok(token);

            _logger.LogError($"Request creating new token failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());
        }
    }
}
