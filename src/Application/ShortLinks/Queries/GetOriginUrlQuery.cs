using MediatR;

namespace Application.ShortLinks.Queries;

public record GetOriginUrlQuery(string ShortLink) : IRequest<string>;