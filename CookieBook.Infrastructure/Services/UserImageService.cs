using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var image = new UserImage(command.ImageContent);

            await _context.UserImages.AddAsync(image);
            user.UserImage = image;

            await _context.SaveChangesAsync();

			return image;
		}

        public async Task UpdateAsync(UpdateImage command, User user)
        {
            user.UserImage.Update(command.ImageContent);
            await _context.SaveChangesAsync();
        }

        public async Task<UserImage> GetByUserIdAsync(int userId)
        {
            var image = await _context.UserImages.GetByUserId(userId).SingleOrDefaultAsync();

            if (image == null)
                throw new CorruptedOperationException("Image doesn't exist");

			return image;
		}

        public async Task<bool> ExistsForUser(int userId) =>
            await _context.UserImages.ExistsForUserAsync(userId);
    }
}