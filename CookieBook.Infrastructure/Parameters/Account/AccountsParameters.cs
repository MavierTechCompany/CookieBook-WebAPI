using System;

namespace CookieBook.Infrastructure.Parameters.Account
{
    public class AccountsParameters
    {
        public DateTime RegistrationDate { get; set; }
        public string Query { get; set; }
        public string Fields { get; set; }
    }
}