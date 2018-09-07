using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Extensions;
using FluentValidation;

namespace CookieBook.Infrastructure.Validators.UserImage
{
    public class UpdateUserImageValidator : AbstractValidator<UpdateImage>
    {
        public UpdateUserImageValidator()
        {
            RuleFor(req => req.ImageContent)
                .NotEmpty()
                .Matches(RegularExpressions.Base64)
                .WithMessage(@"This image is not valid!");
        }
    }
}