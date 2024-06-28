using MediatR;

namespace Application.ShortLinks.Commands;

public record DeleteShortLinkCommand(uint Id) : IRequest<bool>;

