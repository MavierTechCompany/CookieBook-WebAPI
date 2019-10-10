using System;
using System.Collections.Generic;

namespace CookieBook.Infrastructure.DTO
{
    public class UserDto
    {
		public UInt64 UserEmail { get; set; }
		public UserImageDto UserImage { get; set; }
		public virtual ICollection<RecipeDto> Recipes { get; set; }
    }
}