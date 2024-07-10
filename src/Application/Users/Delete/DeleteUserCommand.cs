using MediatR;

namespace Application.Users.Delete;

public record DeleteUserCommand(int Id) : IRequest<bool>;

