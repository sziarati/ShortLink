using MediatR;

namespace Application.ShortLinks.Commands;

public record ExpireShortLinkCommand(int id) : IRequest<bool>;

