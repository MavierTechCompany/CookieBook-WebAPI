using System;

namespace CookieBook.Infrastructure.Parameters.Account
{
    public class AccountsParameters : BaseParameters
    {
        /// <summary>
        /// The creation date you want to search for
        /// </summary>
        /// <example>2020-02-02</example>
        public DateTime RegistrationDate { get; set; }
    }
}