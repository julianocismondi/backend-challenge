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
        public async Task<IActionResult> Login([FromBody] GenerateTokenDto generateTokenDto)
        {
            var userAuthenticate = await _authService.GenerateTokenAsync(generateTokenDto.Email, generateTokenDto.Password);

            //var jsonToken = JsonConvert.SerializeObject(tokenString);
            //Almacenar y enviar en una cookie
            //HttpContext.Response.Cookies.Append(
            //    "SSID",
            //    "Bearer " + tokenString,
            //    new CookieOptions
            //    {
            //        Expires = DateTime.Now.AddDays(7),
            //        HttpOnly = false,
            //        Secure = false,
            //        Path = "../challenge-frontend"
            //    });

            //HttpContext.Response.Cookies.Append("token", tokenString);

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
