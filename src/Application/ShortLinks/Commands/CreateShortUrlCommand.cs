using MediatR;

namespace Application.ShortLinks.Commands;

public record CreateShortLinkCommand(string Name, string OriginalUrl, int UserId) : IRequest<int>;
