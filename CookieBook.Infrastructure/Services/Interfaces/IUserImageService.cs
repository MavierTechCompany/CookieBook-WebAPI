using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Picture;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserImageService
    {
        Task<UserImage> AddAsync(CreateImage command, User user);

        Task UpdateAsync(UpdateImage command, User user);

        Task<UserImage> GetByUserIdAsync(int userId, bool asNoTracking = false);

        Task<bool> ExistsForUser(int userId);
    }
}