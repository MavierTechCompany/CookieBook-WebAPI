namespace CookieBook.Infrastructure.DTO.Base
{
    /// <summary>
    /// Represents an image
    /// </summary>
    public class ImageDto : EntityDto
    {
        /// <summary>
        /// Content of an image as Base64 string
        /// </summary>
        /// <example>iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==</example>
		public string ImageContent { get; set; }
    }
}