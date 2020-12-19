using CookieBook.Infrastructure.Commands.Account;
using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.User
{
    public class UpdateUserData : UpdateAccountData
    {
        /// <summary>
        /// Hash of the email represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1135640108140129452</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string UserEmail { get; set; }
    }
}