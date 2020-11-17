using System.ComponentModel.DataAnnotations;

namespace CookieBook.Infrastructure.Commands.Account
{
    /// <summary>
    /// Represents a set of data used to update a password
    /// </summary>
    public class UpdatePassword
    {
        /// <summary>
        /// Hash of the current password represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1135640218141123452</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string Password { get; set; }

        /// <summary>
        /// Hash of the new password represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1035640218001175364</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string NewPassword { get; set; }
    }
}