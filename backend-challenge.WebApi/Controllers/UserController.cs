﻿using backend_challenge.Business.Dtos;
using backend_challenge.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetListAsync();
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result = await _userService.GetAsync(id);

            return result == null ? NotFound(new
            {
                ErrorMessage = "Usuario no encontrado"
            }) : Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(userDto);
            };

            if (await _userService.ValidateUserExist(userDto.Email))
            {
                ModelState.AddModelError("ErrorMessage", "El email ya se encuentra registrado");
                return BadRequest(ModelState);
            }

            await _userService.CreateAsync(userDto);
            return NoContent();
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var response = await _userService.Delete(id);
            if (!response)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var response = await _userService.Update(updateUserDto);

            return Ok(response);
        }
    }
}