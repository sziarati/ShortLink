using Application.Results;
using MediatR;

namespace Application.ShortLinks.Delete;

public record DeleteShortLinkCommand(int Id) : IRequest<bool>;

