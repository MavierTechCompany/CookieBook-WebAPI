using System.Threading.Tasks;
using CookieBook.Infrastructure.Extensions.Security;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers
{
    public class TestController : ApiControllerBase
    {
        private readonly IDataHashManager _service;

        public TestController(IDataHashManager service)
        {
            _service = service;
        }

        [HttpGet("testHash")]
        public IActionResult TestHashMethods()
        {
            var password = "password";
            byte[] salt;
            byte[] hash;
            _service.CalculatePasswordHash(password, out hash, out salt);
            var passwordResoult = _service.VerifyPasswordHash(password, hash, salt);

            var data = "adam.nowak@gmail.com";
            var dataHash = _service.CalculateDataHash(data);
            var dataResoult = _service.VerifyDataHash(data, dataHash);

            return Json($"{passwordResoult}, {dataResoult}");
        }
    }
}