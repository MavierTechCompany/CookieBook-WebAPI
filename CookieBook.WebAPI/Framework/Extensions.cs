using Microsoft.AspNetCore.Builder;

namespace CookieBook.WebAPI.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware(typeof(ErrorHandlerMiddleware));

        public static IApplicationBuilder UseAccountStatusChecker(this IApplicationBuilder builder) =>
            builder.UseMiddleware(typeof(AccountStatusCheckerMiddleware));
    }
}