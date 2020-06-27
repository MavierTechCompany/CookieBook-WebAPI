using CookieBook.Infrastructure.DTO.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.DTO
{
    public class RateDto : EntityDto
    {
        public float Value { get; set; }
        public int RecipeId { get; set; }
        public string Description { get; set; }
    }
}