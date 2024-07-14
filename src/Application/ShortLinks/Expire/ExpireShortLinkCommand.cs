using Application.Results;
using MediatR;

namespace Application.ShortLinks.Expire;

public record ExpireShortLinkCommand(int id) : IRequest<Result<bool>>;

