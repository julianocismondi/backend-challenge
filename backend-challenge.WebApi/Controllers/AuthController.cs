using backend_challenge.Business.Dtos;
using backend_challenge.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] GenerateTokenDto generateTokenDto)
        {
            var userAuthenticate = await _authService.GenerateTokenAsync(generateTokenDto.Email, generateTokenDto.Password);

            return Ok(userAuthenticate);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProfile()
        {
            var token = Request.Headers.Authorization;
            var response = await _authService.GetProfile(token);
            return Ok(response);
        }
    }
}
