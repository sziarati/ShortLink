using MediatR;

namespace Application.ShortLinks.Commands;

public record DeleteShortLinkCommand(int Id) : IRequest<bool>;

