using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Profiles
{
    public class RecipeImageProfile : Profile
    {
		public RecipeImageProfile() => CreateMap<RecipeImage, RecipeImageDto>();
	}
}