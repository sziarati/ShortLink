using MediatR;

namespace Application.ShortLinks.Queries;

public record GetUniqueCodeQuery(string OriginUrl) : IRequest<string>;
