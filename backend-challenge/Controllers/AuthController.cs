using backend_challenge.Models.Dto;
using backend_challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> GenerateToken([FromBody] AuthDto authDto)
        {
            var response = await _authService.GenerateTokenAsync(authDto.Email, authDto.Password);
            var responseJson = JsonConvert.SerializeObject(response);
            return Ok(responseJson);
        }
    }
}
