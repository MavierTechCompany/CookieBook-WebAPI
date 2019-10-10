using System;

namespace CookieBook.Infrastructure.DTO.Base
{
    public class EntityDto
    {
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
    }
}