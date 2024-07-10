using FluentValidation;

namespace Application.ShortLinks.Create;

public class CreateShortLinkValidator : AbstractValidator<CreateShortLinkCommand>
{
    public CreateShortLinkValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty();

        RuleFor(command => command.OriginUrl)
            .NotEmpty();
    }
}
