using CookieBook.Infrastructure.Commands.Account;
using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.User
{
    public class CreateUser : CreateAccount
    {
        /// <summary>
        /// Hash of the login represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1035640218001175364</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string Login { get; set; }

        /// <summary>
        /// Hash of the email represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1035640218001175364</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string Email { get; set; }
    }
}