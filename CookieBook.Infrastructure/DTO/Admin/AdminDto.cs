using CookieBook.Infrastructure.DTO.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.DTO.Admin
{
    public class AdminDto : EntityDto
    {
        public string Nick { get; set; }
        public string Login { get; set; }
    }
}