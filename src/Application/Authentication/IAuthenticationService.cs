using Application.Results;
using Domain.Entities.UserAggregate;
using Domain.Entities.ValueObjects;
using System.Text.Json;

namespace Application.Authentication;

public interface IAuthenticationService
{
    Task<Result<Token>> Login(string username, Password password);
    Result<CurrentUser> GetCurrentUser(Token token);
    private Token CreateJWTToken(User user)
    {
        var token = new Token // todo jwt , remove password
        {
            AccessToken = JsonSerializer.Serialize(user),
        };

        return token;
    }
}
