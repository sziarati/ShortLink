using FluentValidation;

namespace Application.ShortLinks.Expire;

public class ExpireShortValidator : AbstractValidator<ExpireShortLinkCommand>
{
    public ExpireShortValidator()
    {
        RuleFor(command => command.id)
            .NotEmpty()
            .NotNull();
    }
}
