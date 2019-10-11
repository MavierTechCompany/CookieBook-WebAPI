using System.Linq;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
			CreateMap<Recipe, RecipeDto>()
				.ForMember(dto => dto.Categories, opt => opt.MapFrom(x => x.RecipeCategories.Select(y => y.Category).ToList()));
		}
    }
}