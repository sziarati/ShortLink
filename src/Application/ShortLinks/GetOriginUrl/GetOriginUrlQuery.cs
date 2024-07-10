using Application.Results;
using MediatR;

namespace Application.ShortLinks.GetOriginUrl;

public record GetOriginUrlQuery(string UniqueCode) : IRequest<Result<string>>;