using System.Threading.Tasks;
using CookieBook.Infrastructure.Commands.Account;
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

        public UserPasswordController(IUserService userService)
        {
			_userService = userService;
        }

		[Authorize(Roles = "user")]
		[HttpPut]
		public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePassword command)
		{
			if (id != AccountID)
				return Forbid();

			await _userService.UpdatePasswordAsync(id, command);
			return NoContent();
		}
    }
}