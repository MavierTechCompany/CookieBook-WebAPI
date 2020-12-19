using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookieBook.Infrastructure.Commands.User
{
    public class BlockUser
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