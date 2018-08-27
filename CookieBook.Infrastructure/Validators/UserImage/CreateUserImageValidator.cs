using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.UserImage
{
    public class CreateUserImageValidator : AbstractValidator<CreateImage>
    {
        public CreateUserImageValidator()
        {
            RuleFor(req => req.ImageContent)
                .NotEmpty()
                .Matches(RegularExpressions.Base64)
                .WithMessage(@"This image is not valid!");
        }
    }
}