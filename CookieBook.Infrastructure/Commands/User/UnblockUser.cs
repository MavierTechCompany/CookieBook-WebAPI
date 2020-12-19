using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookieBook.Infrastructure.Commands.User
{
    public class UnblockUser
    {
        /// <summary>
        /// Hash of the email represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>1035640218001175364</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string Email { get; set; }

        /// <summary>
        /// Hash of the new password represented by 64-bit unsigned int (ulong), converted to string
        /// </summary>
        /// <example>103564021800117364</example>
        [Required]
        [MinLength(19)]
        [MaxLength(20)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Restore key, needed to unlock an account
        /// </summary>
        /// <example>251T3@?rE!DN</example>
        [Required]
        public string RestoreKey { get; set; }
    }
}