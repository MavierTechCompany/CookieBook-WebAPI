namespace CookieBook.Infrastructure.Extensions
{
    public static class RegularExpressions
    {
        public static string Nick { get; } = @"^[A-Za-z][A-Za-z0-9]+$";
    }
}