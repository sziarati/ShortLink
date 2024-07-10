using Application.Results;
using Domain.Entities.UserAggregate;
using MediatR;
using ValueObjects = Domain.Entities.ValueObjects;

namespace Application.Users.Create;

public record CreateUserCommand(string UserName, ValueObjects.Email Email, ValueObjects.Password Password, ValueObjects.Password RePassword, ValueObjects.Address Address) 
    : IRequest<Result<User>>;

