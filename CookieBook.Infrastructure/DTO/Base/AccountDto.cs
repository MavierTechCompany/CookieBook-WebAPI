using System;

namespace CookieBook.Infrastructure.DTO.Base
{
	public class AccountDto : EntityDto
	{
		public string Nick { get; set; }
		public UInt64 Login { get; set; }
		public string Role { get; set; }
	}
}