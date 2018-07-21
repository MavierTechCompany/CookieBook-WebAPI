using System.Threading.Tasks;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers
{
    public class TestController : ApiControllerBase
    {
        private readonly IJwtHandler _jwtHandler;

        public TestController(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetTokenAsync()
        {
            var token = await _jwtHandler.CreateTokenAsync(1, "admin");
            return Json(token);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAdminAsync()
        {
            return await Task.FromResult(Json($"{AccountID}. Adam Nowak"));
        }

        [HttpGet("user")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetUserAsync()
        {
            return await Task.FromResult(Json($"{AccountID}. Anna Nowak"));
        }
    }
}