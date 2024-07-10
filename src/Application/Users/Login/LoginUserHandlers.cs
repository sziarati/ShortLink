using Application.Authentication;
using Application.Results;
using Application.Users.Login;
using MediatR;

namespace Application.Users.Handlers;

public class LoginUserHandlers(
    IAuthenticationService authenticationService) :

    IRequestHandler<LoginUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var loginResult = await authenticationService.Login(request.UserName, request.Password);
        return loginResult.IsSuccess ? Result<string>.Success(loginResult.Data.AccessToken) : Result<string>.Failure(Errors.LoginFailedError);
    }
}