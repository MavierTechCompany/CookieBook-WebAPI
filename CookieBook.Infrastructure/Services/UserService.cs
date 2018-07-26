using System.Threading.Tasks;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Services.Interfaces;

namespace CookieBook.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly CookieContext _context;

        public UserService(CookieContext context)
        {
            _context = context;
        }

        public Task AddUserAsync(AddUser command)
        {
            throw new System.NotImplementedException();
        }
    }
}