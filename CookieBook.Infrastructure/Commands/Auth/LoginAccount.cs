using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.Auth
{
    public class LoginAccount
    {
        /// <summary>
        /// Hash of the login or email represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1135640108140129452</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string LoginOrEmail { get; set; }

        /// <summary>
        /// Hash of the password represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1135640108140129452</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}