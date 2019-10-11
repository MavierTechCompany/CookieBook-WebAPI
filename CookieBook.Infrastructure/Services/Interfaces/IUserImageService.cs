using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserImageService
    {
        Task<UserImageDto> AddAsync(CreateImage command, User user);
        Task UpdateAsync(UpdateImage command, User user);
        Task<UserImageDto> GetByUserIdAsync(int userId);
        Task<bool> ExistsForUser(int userId);
    }
}