
using Dotnet9CleanArchitecture.Application.Auth.Models;
using FluentValidation;

namespace Dotnet9CleanArchitecture.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
