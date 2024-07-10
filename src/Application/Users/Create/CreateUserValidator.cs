using Application.Results;
using FluentValidation;

namespace Application.Users.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(command => command.UserName)
            .NotEmpty().WithMessage(Errors.InvalidUserNameError.Value);

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage(Errors.InvalidPasswordError.Value)
            .Equal(command => command.RePassword).WithMessage(Errors.InvalidRepeatedPasswordError.Value);

        RuleFor(command => command.RePassword)
            .NotEmpty().WithMessage(Errors.InvalidPasswordError.Value);

        RuleFor(command => command.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(Errors.InvalidEmailError.Value);
    }
}
