using FluentValidation;

namespace Ims.Application.Users;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        _ = RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email!");

        _ = RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Must(BeValidPassword)
            .WithMessage("Password must contain at least 6 characters with capital letter and number!");
    }

    private bool BeValidPassword(string password)
    {
        return !string.IsNullOrEmpty(password) && password.Any(char.IsUpper) && password.Any(char.IsDigit);
    }
}
