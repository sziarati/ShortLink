using FluentValidation;

namespace Application.ShortLinks.Delete;

public class DeleteShortValidator : AbstractValidator<DeleteShortLinkCommand>
{
    public DeleteShortValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .NotNull();
    }
}
