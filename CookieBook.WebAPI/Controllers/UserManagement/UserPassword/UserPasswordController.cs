using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using CookieBook.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.UserManagement.UserPassword
{
    [Route("user-management/users/{id}/password")]
    [AccessableByInactiveAccount(false)]
    public class UserPasswordController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserPasswordController(IUserService userService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
        }

        /// <summary>
        /// Updates user password
        /// </summary>
        /// <param name="id" example="1">Id of the user that wants to change his/her password</param>
        /// <param name="command"></param>
        [HttpPut]
        [Authorize(Roles = "user")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePassword command)
        {
            if (id != AccountID)
                return Forbid();

            var user = await _userService.GetAsync(id);
            if (user.IsActive == false)
                throw new CorruptedOperationException("Invalid operation");

            await _userService.UpdatePasswordAsync(id, command);
            return NoContent();
        }
    }
}