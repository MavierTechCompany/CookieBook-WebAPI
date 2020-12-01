using CookieBook.Infrastructure.Extensions;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.Picture
{
    public class CreateImage
    {
        /// <summary>
        /// Content of an image as Base64 string
        /// </summary>
        /// <example>iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==</example>
        [Required]
        [RegularExpression(RegularExpressions.Base64)]
        public string ImageContent { get; set; }
    }
}