using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.Base
{
    [Route("[controller]")]
    public class ApiControllerBase : Controller
    {
        protected readonly IMapper _mapper;
        protected int AccountID => User?.Identity?.IsAuthenticated == true ? int.Parse(User.Claims.ToList().First().Value) : 0;

        public ApiControllerBase(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}