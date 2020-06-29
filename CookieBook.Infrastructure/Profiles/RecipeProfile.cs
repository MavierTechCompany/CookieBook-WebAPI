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
                .ForMember(dto => dto.AverageRate, opt => opt.MapFrom(src => src.Rates.Average(x => x.Value)))
                .ForMember(dto => dto.Categories, opt => opt.MapFrom(src => src.RecipeCategories.Select(x => x.Category).ToList()));
        }
    }
}