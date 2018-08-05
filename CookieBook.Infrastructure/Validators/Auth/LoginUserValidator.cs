using CookieBook.Infrastructure.Commands.Auth;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.Auth
{
    public class LoginUserValidator : AbstractValidator<LoginUser>
    {
        public LoginUserValidator() { }
    }
}