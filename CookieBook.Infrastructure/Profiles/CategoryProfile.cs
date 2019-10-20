using System.Linq;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
			CreateMap<Component, ComponentDto>();
			CreateMap<Recipe, RecipeForCategoryDto>();
			CreateMap<Category, CategoryDto>()
				.ForMember(dto => dto.Recipes, opt => opt.MapFrom(x => x.RecipeCategories.Select(y => y.Recipe).ToList()));
		}
    }
}