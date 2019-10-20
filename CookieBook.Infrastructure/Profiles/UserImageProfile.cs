using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Profiles
{
    public class UserImageProfile : Profile
    {
		public UserImageProfile() => CreateMap<UserImage, UserImageDto>();
	}
}