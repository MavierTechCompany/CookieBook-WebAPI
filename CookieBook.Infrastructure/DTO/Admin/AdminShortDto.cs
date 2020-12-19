using CookieBook.Infrastructure.DTO.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.DTO.Admin
{
    public class AdminShortDto : EntityDto
    {
        /// <summary>
        /// Admin nick that will be displayed to everyone
        /// </summary>
        /// <example>Steave77</example>
        public string Nick { get; set; }
    }
}