using System.Threading.Tasks;
using CookieBook.Infrastructure.Commands.User;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(AddUser command);
    }
}