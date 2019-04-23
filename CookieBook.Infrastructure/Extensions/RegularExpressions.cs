namespace CookieBook.Infrastructure.Extensions
{
    public static class RegularExpressions
    {
        public static string Nick { get; } = @"^[A-Za-z][A-Za-z0-9]+$";
        public static string WholeSentence { get; } = @"[A-Z][^.?!]+((?![.?!]\s[A-Z]))+";
        public static string Base64 { get; } = @"^[a-zA-Z0-9\+/]*={0,3}$";
    }
}