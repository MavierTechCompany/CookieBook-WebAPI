using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CookieBook.WebAPI.Controllers
{
    [Route("user-management")]
    public class UserManagementController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserImageService _userImageService;

        public UserManagementController(IUserService userService, IUserImageService userImageService)
        {
            _userService = userService;
            _userImageService = userImageService;
        }

        #region USERS

        [HttpPost("users")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser command)
        {
            var user = await _userService.AddAsync(command);
            return Created($"/users/{user.Id}", user);
        }

        [Authorize(Roles = "user")]
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserData command)
        {
            if (id != AccountID)
                return Forbid();

            await _userService.UpdateAsync(id, command);
            return NoContent();
        }

        #endregion

        #region USERS/TOKEN

        [HttpPost("users/token")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUser command)
        {
            var token = await _userService.LoginAsync(command);
            return Ok(token);
        }

        #endregion

        #region USERS/id/PASSWORD

        [Authorize(Roles = "user")]
        [HttpPut("users/{id}/password")]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePassword command)
        {
            if (id != AccountID)
                return Forbid();

            await _userService.UpdatePasswordAsync(id, command);
            return NoContent();
        }

        #endregion

        #region USERS/id/IMAGE

        [Authorize(Roles = "user")]
        [HttpPost("users/{id}/image")]
        public async Task<IActionResult> CreateImageAsync(int id, [FromBody] CreateImage command)
        {
            if (id != AccountID)
                return Forbid();
            if (await _userImageService.ExistsForUser(id) == true)
                return BadRequest("Image already exists.");

            var user = await _userService.GetAsync(id);
            var image = await _userImageService.AddAsync(command, user);

            var resoultImage = new { image.Id, image.UserRef, image.ImageContent };
            return Created($"/users/{id}/image/{user.Id}", resoultImage);
        }

        [Authorize(Roles = "user")]
        [HttpPut("users/{id}/image")]
        public async Task<IActionResult> UpdateImageAsync(int id, [FromBody] UpdateImage command)
        {
            if (id != AccountID)
                return Forbid();
            if (await _userImageService.ExistsForUser(id) == false)
                return BadRequest("Image doesn't exist.");

            var user = await _userService.GetAsync(id);
            await _userImageService.UpdateAsync(command, user);
            return NoContent();
        }

        #endregion
    }
}