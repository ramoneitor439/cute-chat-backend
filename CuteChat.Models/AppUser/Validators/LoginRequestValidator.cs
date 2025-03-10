using FluentValidation;

namespace CuteChat.Models.AppUser.Validators;
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("email address cannot be empty or null")
            .EmailAddress()
            .WithMessage("email address have an invalid format");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("password cannot be empty or null");
    }
}
