using MediatR;

namespace Application.ShortLinks.Commands;

public record CreateShortLinkCommand(string Name, string OriginUrl, int UserId) : IRequest<int>;
