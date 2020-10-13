using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Filters
{
    /// <summary>
    /// Resource filter informing whether the action can be performed by an inactive or active user.
    /// </summary>
    public class AccessableByInactiveAccountAttribute : Attribute, IAsyncResourceFilter
    {
        private bool _canAccess;

        public AccessableByInactiveAccountAttribute(bool canAccess) => _canAccess = canAccess;

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var accountService = context.HttpContext.RequestServices.GetService<IAccountService>();
            string bearer = context.HttpContext.Request.Headers["Authorization"];

            var isActive = await CheckIfActive(bearer, accountService);

            if (isActive != _canAccess)
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

        private async Task<bool> CheckIfActive(string bearer, IAccountService accountService)
        {
            bool isActive = true;

            if (!string.IsNullOrEmpty(bearer))
            {
                var accountId = GetAccountId(bearer);
                isActive = await accountService.IsActive(accountId);
            }

            return isActive;
        }

        private int GetAccountId(string bearer)
        {
            var jwt = bearer.Split(" ")[1];

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return int.Parse(token.Claims.First().Value);
        }
    }
}