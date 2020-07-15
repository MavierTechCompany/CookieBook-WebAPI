using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDto>().ForSourceMember(x => x.Login, opt => opt.DoNotValidate());
            CreateMap<Admin, AdminShortDto>();
        }
    }
}