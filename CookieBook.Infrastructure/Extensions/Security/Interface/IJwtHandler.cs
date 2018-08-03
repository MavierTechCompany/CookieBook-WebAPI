using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Extensions.Security.Interface
{
    public interface IJwtHandler
    {
        Task<string> CreateTokenAsync(int userId, string role);
    }
}