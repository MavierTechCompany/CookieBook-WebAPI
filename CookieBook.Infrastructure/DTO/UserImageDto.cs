using CookieBook.Infrastructure.DTO.Base;

namespace CookieBook.Infrastructure.DTO
{
    public class UserImageDto : ImageDto
    {
        /// <summary>
        /// Id of the user that this image belongs to
        /// </summary>
        /// <example>1</example>
        public int? UserRef { get; set; }
    }
}