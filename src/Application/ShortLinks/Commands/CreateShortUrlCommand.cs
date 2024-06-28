using Domain.Entities.ShortLinkAggregate;
using MediatR;

namespace Application.ShortLinks.Commands;

public record CreateShortLinkCommand(string Name, string OriginalUrl, uint UserId) : IRequest<uint>;

