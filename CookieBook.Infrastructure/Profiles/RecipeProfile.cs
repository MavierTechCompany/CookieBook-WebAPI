using System.Collections.Generic;
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
                .ForMember(dto => dto.Rates, opt => opt.MapFrom(src => src.Rates.Where(x => x.Deleted == false)))
                .ForMember(dto => dto.AverageRate, opt => opt.MapFrom(src => CalculateAverageRate(src.Rates.Where(x => x.Deleted == false).ToList())))
                .ForMember(dto => dto.Categories, opt => opt.MapFrom(src => src.RecipeCategories.Select(x => x.Category).ToList()));
        }

        private float CalculateAverageRate(ICollection<Rate> rates) => rates == null || rates.Count == 0 ? 0.0f : rates.Average(x => x.Value);
    }
}