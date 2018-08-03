using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.User;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AddUserAsync(CreateUser command);
        Task<bool> UserExistsInDatabaseAsync(string nick, ulong login, ulong email);
    }
}