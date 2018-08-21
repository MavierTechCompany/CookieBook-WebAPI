using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.User;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AddAsync(CreateUser command);
        Task UpdateAsync(UpdateUserData command);
        Task<string> LoginAsync(LoginUser command);
    }
}