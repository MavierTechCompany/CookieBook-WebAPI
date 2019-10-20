using System;
using System.Collections.Generic;
using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
	public class UserDto : AccountDto
	{
		public UInt64 UserEmail { get; set; }
		public UserImageDto UserImage { get; set; }
		public virtual ICollection<RecipeDto> Recipes { get; set; }
	}
}