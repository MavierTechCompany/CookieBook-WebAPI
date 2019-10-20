using System.ComponentModel;
using AutoMapper;
using CookieBook.Infrastructure.DTO;

namespace CookieBook.Infrastructure.Profiles
{
    public class ComponentProfile : Profile
    {
		public ComponentProfile() => CreateMap<Component, ComponentDto>();
	}
}