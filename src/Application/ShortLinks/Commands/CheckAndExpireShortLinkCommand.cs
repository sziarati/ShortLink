using MediatR;

namespace Application.ShortLinks.Commands;

public record CheckAndExpireShortLinkCommand() : IRequest<bool>;

