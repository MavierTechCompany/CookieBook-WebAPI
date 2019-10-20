using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class UserImageDto : ImageDto
    {
        public int? UserRef { get; set; }
    }
}