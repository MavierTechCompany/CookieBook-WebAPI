using AutoMapper;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using CookieBook.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.UserManagement.UserRestoreKey
{
    [Route("user-management/users/")]
    public class UserRestoreKeyController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserRestoreKeyController(IMapper mapper, IUserService userService) : base(mapper) => _userService = userService;

        /// <summary>
        /// Locks the user if they forgot their password and wants to restore it.
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returned if the user was successfully locked.</response>
        /// <response code="400">Returned if the user doesn't exist.</response>
        [HttpPost("block")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> BlockUserAsync([FromBody] BlockUser command)
        {
            await _userService.BlockAsync(command);
            return Ok();
        }

        /// <summary>
        /// Unlocks previously locked user.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <response code="200">Returned if the user was successfully unlocked.</response>
        /// <response code="400">Returned if the user doesn't exist or can't be unlocked.</response>
        [HttpPost("unblock")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UnblockUserAsync([FromBody] UnblockUser command)
        {
            await _userService.UnblockAsync(command);
            return Ok();
        }

        /// <summary>
        /// Generates new restore key for user with given ID
        /// </summary>
        /// <param name="id" example="2">ID of the user that wants to generate new restore key</param>
        /// <returns></returns>
        /// <response code="200">Returns new restore key</response>
        /// <response code="400">Returned if the user doesn't exist or can't generate new restore key</response>
        /// <response code="403">Returned if the user is locked or wants to generate new restore key for someone else.</response>
        [Authorize("user")]
        [HttpGet("{id}/restore-key")]
        [AccessableByInactiveAccount(false)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GenerateRestoreKeyAsync(int id)
        {
            if (id != AccountID)
                return Forbid();

            var key = await _userService.GenerateNewRestoreKey(id);
            return Ok(new { RestoreKey = key });
        }
    }
}