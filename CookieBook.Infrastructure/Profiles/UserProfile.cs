using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
		public UserProfile() => CreateMap<User, UserDto>();
	}
}