using Application.Results;
using Domain.Entities.ShortLinkAggregate;
using MediatR;

namespace Application.ShortLinks.Create;
public record CreateShortLinkCommand(string Name, string OriginUrl, string UserName) : IRequest<Result<ShortLink>>;
