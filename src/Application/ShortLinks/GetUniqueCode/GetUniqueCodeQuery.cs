using Application.Results;
using MediatR;

namespace Application.ShortLinks.GetUniqueCode;

public record GetUniqueCodeQuery(string OriginUrl) : IRequest<Result<string>>;
