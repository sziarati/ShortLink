using MediatR;

namespace Application.ShortLinks.Expire;

public record ExpireShortLinkCommand(int id) : IRequest<bool>;

