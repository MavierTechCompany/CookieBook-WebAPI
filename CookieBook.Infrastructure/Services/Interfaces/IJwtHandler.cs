using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(int userId, string role);
    }
}