using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.UserManagement.UserPassword
{
    [Route("user-management/users/{id}/password")]
    public class UserPasswordController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserPasswordController(IUserService userService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
        }

        [Authorize(Roles = "user")]
        [HttpPut]
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