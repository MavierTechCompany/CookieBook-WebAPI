using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Filters
{
    public class AccessableByInactiveAccountResourceFilter : IAsyncResourceFilter
    {
        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            //TODO Implement code from access middleware and ad a bool parameter
            throw new NotImplementedException();
        }
    }
}