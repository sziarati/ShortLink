using Application.Results;
using Domain.Entities.ShortLinkAggregate;
using MediatR;

namespace Application.ShortLinks.Create;

public record CreateShortLinkCommand(string Name, string OriginUrl) : IRequest<Result<ShortLink>>;
