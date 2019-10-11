using System;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Services
{
    public class UserImageService : IUserImageService
    {
        private readonly CookieContext _context;
		private readonly IMapper _mapper;

		public UserImageService(CookieContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
		}

        public async Task<UserImageDto> AddAsync(CreateImage command, User user)
        {
            var image = new UserImage(command.ImageContent);

            await _context.UserImages.AddAsync(image);
            user.UserImage = image;

            await _context.SaveChangesAsync();

			return _mapper.Map<UserImageDto>(image);
		}

        public async Task UpdateAsync(UpdateImage command, User user)
        {
            user.UserImage.Update(command.ImageContent);
            await _context.SaveChangesAsync();
        }

        public async Task<UserImageDto> GetByUserIdAsync(int userId)
        {
            var image = await _context.UserImages.GetByUserId(userId).SingleOrDefaultAsync();

            if (image == null)
                throw new CorruptedOperationException("Invalid id");

			return _mapper.Map<UserImageDto>(image);
		}

        public async Task<bool> ExistsForUser(int userId) =>
            await _context.UserImages.ExistsInDatabaseAsync(userId);
    }
}