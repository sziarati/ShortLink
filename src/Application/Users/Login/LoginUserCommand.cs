using Application.Results;
using MediatR;

namespace Application.Users.Login;

public record LoginUserCommand(string UserName, string Password) : IRequest<Result<string>>;
