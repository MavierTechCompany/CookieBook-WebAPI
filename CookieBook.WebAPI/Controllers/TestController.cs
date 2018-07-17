using System.Threading.Tasks;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IJwtHandler _jwtHandler;

        public TestController(IJwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpGet("token")]
        public IActionResult GetTokenAsync()
        {
            var token = _jwtHandler.CreateToken(1, "admin");
            return Json(token);
        }
    }
}