using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.Account
{
    public class UpdateAccountData
    {
        /// <summary>
        /// Hash of the login represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1135640108140129452</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string Login { get; set; }
    }
}