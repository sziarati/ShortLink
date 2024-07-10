using Domain.Entities.ShortLinkAggregate;
using MediatR;

namespace Application.Users.GetUserShortLinks;

public record GetUserShortLinksQuery(string UserName) : IRequest<List<ShortLink>>;
