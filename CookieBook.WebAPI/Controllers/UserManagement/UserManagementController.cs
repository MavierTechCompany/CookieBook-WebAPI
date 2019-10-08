using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Account;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.UserManagement
{
	[Route("user-management/users")]
    public class UserManagementController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        #region USERS
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser command)
        {
            var user = await _userService.AddAsync(command);
            return Created($"{Request.Host}{Request.Path}/{user.Id}", user);
        }

        [HttpGet]
        public async Task<IActionResult> ReadUsersAsync(AccountsParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) &&
                !PropertyManager.PropertiesExists<User>(parameters.Fields))
            {
                return BadRequest();
            }

            var users = await _userService.GetAsync(parameters);
            return Ok(users.ShapeData(parameters.Fields));
        }
		#endregion

		#region USER
		[HttpGet("{id}")]
		public async Task<IActionResult> ReadUserAsync(int id, [FromQuery] string fields)
		{
			if (!string.IsNullOrWhiteSpace(fields) &&
				!PropertyManager.PropertiesExists<User>(fields))
			{
				return BadRequest();
			}

			var user = await _userService.GetAsync(id);
			return Ok(user.ShapeData(fields));
		}

		[Authorize(Roles = "user")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserData command)
		{
			if (id != AccountID)
				return Forbid();

			await _userService.UpdateAsync(id, command);
			return NoContent();
		}
        #endregion

        #region TOKEN
        [HttpPost("token")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUser command)
        {
            var token = await _userService.LoginAsync(command);
            return Ok(token);
        }
        #endregion
    }
}