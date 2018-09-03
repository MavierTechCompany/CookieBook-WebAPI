using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
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
            try
            {
                var user = await _userService.AddAsync(command);
                return Created($"/users/{user.Id}", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region USERS/TOKEN

        [HttpPost("users/token")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUser command)
        {
            try
            {
                var token = await _userService.LoginAsync(command);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region USERS/id/PASSWORD

        [Authorize(Roles = "user")]
        [HttpPut("users/{id}/password")]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdateUserData command)
        {
            if (id != AccountID)
                return Forbid();

            try
            {
                await _userService.UpdateAsync(id, command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region USERS/id/IMAGE

        [Authorize(Roles = "user")]
        [HttpPost("users/{id}/image")]
        public async Task<ActionResult> CreateImageAsync(int id, [FromBody] CreateImage command)
        {
            if (id != AccountID)
                return Forbid();
            if (await _userImageService.ExistsForUser(id) == true)
                return BadRequest("Image already exists.");

            try
            {
                var user = await _userService.GetAsync(id);
                var image = _userImageService.AddAsync(command, user);
                return Created($"/users/{id}/image/{user.Id}", image);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}