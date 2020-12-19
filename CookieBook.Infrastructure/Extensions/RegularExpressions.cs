namespace CookieBook.Infrastructure.Extensions
{
    public static class RegularExpressions
    {
        public const string Nick = @"^[A-Za-z][A-Za-z0-9]+$";
        public const string WholeSentence = @"[A-Z][^.?!]+((?![.?!]\s[A-Z]))+";
        public const string Base64 = @"^[a-zA-Z0-9\+/]*={0,3}$";
    }
}