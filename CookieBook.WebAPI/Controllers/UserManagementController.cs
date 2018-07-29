using System.Threading.Tasks;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
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

        [HttpPost("users")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser command)
        {
            try
            {
                await _userService.AddUserAsync(command);
                return StatusCode(201); //Should use Creater(uri, value) instead
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}