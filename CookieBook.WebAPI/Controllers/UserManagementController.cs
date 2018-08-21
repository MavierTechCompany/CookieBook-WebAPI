using System;
using System.Threading.Tasks;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers
{
    [Route("user-management")]
    public class UserManagementController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
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
    }
}