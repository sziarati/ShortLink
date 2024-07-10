using Application.Results;
using MediatR;
using ValueObjects = Domain.Entities.ValueObjects;

namespace Application.Users.ResetPassword;

public record ResetPasswordCommand(string UserName, ValueObjects.Password Password, ValueObjects.Password RePassword) 
    : IRequest<Result<string>>;

