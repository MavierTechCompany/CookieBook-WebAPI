using CookieBook.Infrastructure.Extensions;
using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.Account
{
    public class CreateAccount
    {
        /// <summary>
        /// User nick that will be displayed to everyone
        /// </summary>
        /// <example>Steave77</example>
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(RegularExpressions.Nick)]
        public string Nick { get; set; }

        /// <summary>
        /// Hash of the password represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1035640218001175364</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}