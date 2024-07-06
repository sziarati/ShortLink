using MediatR;

namespace Application.ShortLinks.Queries;

public record GetOriginUrlQuery(string UniqueCode) : IRequest<string>;