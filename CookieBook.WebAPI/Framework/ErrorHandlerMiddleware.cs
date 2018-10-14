using System;
using System.Net;
using System.Threading.Tasks;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CookieBook.WebAPI.Framework
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            Type exceptionType = exception.GetType();
            string response;
            int statusCode;

            switch (exception)
            {
                case Exception ex when exceptionType == typeof(CorruptedOperationException):
                    response = ex.Message;
                    statusCode = (int) HttpStatusCode.BadRequest;
                    break;

                default:
                    response = "Something went wrong.";
                    statusCode = 500;
                    break;
            }

            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}