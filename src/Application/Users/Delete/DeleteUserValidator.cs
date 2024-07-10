using FluentValidation;

namespace Application.Users.Delete;

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .NotNull();
    }
}
