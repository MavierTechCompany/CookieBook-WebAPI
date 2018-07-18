namespace CookieBook.Domain.JWT
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public double ExpiryDays { get; set; }
    }
}