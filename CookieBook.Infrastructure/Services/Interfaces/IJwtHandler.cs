using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IJwtHandler
    {
        Task<string> CreateTokenAsync(int userId, string role);
    }
}