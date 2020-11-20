using System;
using System.Collections.Generic;
using System.Text;

namespace CookieBook.Infrastructure.Parameters
{
    public class BaseParameters
    {
        /// <summary>
        /// String you want to search for. It may be a full phrase or part of it. Search doesn't happen if <b>Query</b> is empty.
        /// </summary>
        /// <example>John</example>
        public string Query { get; set; }

        /// <summary>
        /// Names of entity's fields that caller wants to get. Must contains names that are a part of the given DTO. Names must be separated by a comma
        /// </summary>
        /// <example>Id,CreatedAt</example>
        public string Fields { get; set; }
    }
}