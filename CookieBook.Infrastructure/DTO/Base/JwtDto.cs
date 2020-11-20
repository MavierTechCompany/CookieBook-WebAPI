using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.DTO.Base
{
    /// <summary>
    /// Represents a set of data used in authentication
    /// </summary>
    public class JwtDto
    {
        /// <summary>
        /// JWT token used for user authentication
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
        public string Token { get; set; }
    }
}