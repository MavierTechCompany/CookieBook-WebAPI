using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Profiles
{
    public class RateProfile : Profile
    {
        public RateProfile() => CreateMap<Rate, RateDto>();
    }
}