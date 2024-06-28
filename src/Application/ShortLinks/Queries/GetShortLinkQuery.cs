using MediatR;

namespace Application.ShortLinks.Queries;

public record GetShortLinkQuery(string OriginUrl) : IRequest<string>;
