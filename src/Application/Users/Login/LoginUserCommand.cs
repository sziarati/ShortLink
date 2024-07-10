using Application.Results;
using MediatR;
using ValueObjects = Domain.Entities.ValueObjects;

namespace Application.Users.Login;

public record LoginUserCommand(string UserName, ValueObjects.Password Password) : IRequest<Result<string>>;
