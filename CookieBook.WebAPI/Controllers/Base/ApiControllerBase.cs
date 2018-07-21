using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.Base
{
    [Route("[controller]")]
    public class ApiControllerBase : Controller
    {
        protected int AccountID => User?.Identity?.IsAuthenticated == true ?
            int.Parse(User.Claims.ToList().First().Value) : 0;
    }
}