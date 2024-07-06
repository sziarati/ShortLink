using Application.ShortLinks.Commands;
using FluentValidation;

namespace Application.ShortLinks.Validators;

public class CreateShortValidator : AbstractValidator<CreateShortLinkCommand>
{
    public CreateShortValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty();

        RuleFor(command => command.OriginalUrl)
            .NotEmpty();

        RuleFor(command => command.UserId)
            .NotEmpty()
            .NotNull();
    }
}
