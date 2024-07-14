using Application.Results;
using MediatR;

namespace Application.Users.Delete;

public record DeleteUserCommand(int Id) : IRequest<Result<bool>>;

