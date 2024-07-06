using Application.ShortLinks.Commands;
using FluentValidation;

namespace Application.ShortLinks.Validators;

public class DeleteShortValidator : AbstractValidator<DeleteShortLinkCommand>
{
    public DeleteShortValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .NotNull();
    }
}
