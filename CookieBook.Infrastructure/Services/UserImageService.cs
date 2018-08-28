using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.Services.Interfaces;

namespace CookieBook.Infrastructure.Services
{
    public class UserImageService : IUserImageService
    {
        private readonly CookieContext _context;

        public UserImageService(CookieContext context)
        {
            _context = context;
        }

        public async Task<UserImage> AddAsync(CreateImage command, User user)
        {
            if (await _context.UserImages.ExistsInDatabaseAsync(command.ImageContent) == true)
                throw new Exception("Image already exists.");

            var image = new UserImage(command.ImageContent);
            image.User = user;

            await _context.UserImages.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task UpdateAsync(UpdateImage command)
        {
            throw new NotImplementedException();
        }
    }
}