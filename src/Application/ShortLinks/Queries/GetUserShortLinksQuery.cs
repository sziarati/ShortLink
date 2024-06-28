using Domain.Entities.ShortLinkAggregate;
using MediatR;

namespace Application.ShortLinks.Queries;

public record GetUserShortLinksQuery(Guid UserGuid) : IRequest<List<ShortLink>>;
