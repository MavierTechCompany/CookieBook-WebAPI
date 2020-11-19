using AutoMapper;
using CookieBook.Infrastructure.DTO.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Profiles
{
    public class JwtProfile : Profile
    {
        public JwtProfile()
        {
            CreateMap<string, JwtDto>()
                .ForMember(dto => dto.Token, opt => opt.MapFrom(y => y));
        }
    }
}