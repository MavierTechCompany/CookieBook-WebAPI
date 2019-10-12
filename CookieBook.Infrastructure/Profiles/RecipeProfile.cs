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
			CreateMap<Component, ComponentDto>();
			CreateMap<Category, CategoryForRecipeDto>();
			CreateMap<Recipe, RecipeDto>()
				.ForMember(dto => dto.Components, opt => opt.MapFrom(src => src.Components))
			 	.ForMember(dto => dto.Categories, opt => opt.MapFrom(x => x.RecipeCategories.Select(y => y.Category).ToList()));
		}
    }
}