using Application.Results;
using MediatR;

namespace Application.Users.Login;

public record LoginUserQuery(string UserName, string Password) : IRequest<Result<string>>;
