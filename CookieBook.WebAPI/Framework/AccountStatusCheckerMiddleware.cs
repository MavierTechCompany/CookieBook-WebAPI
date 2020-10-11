using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Framework
{
    public class AccountStatusCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public AccountStatusCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAccountService accountService)
        {
            string bearer = context.Request.Headers["Authorization"];

            if (await CheckIfActive(bearer, accountService))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

        private async Task<bool> CheckIfActive(string bearer, IAccountService accountService)
        {
            bool isActive = true;

            if (!string.IsNullOrEmpty(bearer))
            {
                var jwt = bearer.Split(" ")[1];

                var accountId = GetAccountId(jwt);
                isActive = await accountService.IsActive(accountId);
            }

            return isActive;
        }

        private int GetAccountId(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            return int.Parse(token.Claims.First().Value);
        }
    }
}