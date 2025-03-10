using FluentValidation;

namespace CuteChat.Models.AppUser.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("first name cannot be empty or null")
            .Must(x => x.Length is >= 3 and <= 255)
            .WithMessage("first name must be between 3 and 255 characters")
            .Matches(CustomRegexPatterns.OnlyLetters);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("last name cannot be empty or null")
            .Must(x => x.Length is >= 3 and <= 255)
            .WithMessage("last name must be between 3 and 255 characters")
            .Matches(CustomRegexPatterns.OnlyLetters);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("email address cannot be empty")
            .EmailAddress()
            .WithMessage("email address have an invalid format");

        RuleFor(x => x.MiddleName)
            .Must(x => x.Length is >= 3 and <= 255)
            .WithMessage("middle name must be between 3 and 255 characters")
            .Matches(CustomRegexPatterns.OnlyLetters)
            .WithMessage("middle name is not in a correct format")
            .When(x => !string.IsNullOrEmpty(x.MiddleName));
            

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("phone number cannot be empty")
            .Must(x => x.Length == 8)
            .WithMessage("phone number must have 11 characters")
            .Matches(CustomRegexPatterns.OnlyNumbers)
            .WithMessage("phone number can only have numeric characters");
    }
}
