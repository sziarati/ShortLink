using Application.Results;
using Domain.Entities.UserAggregate;
using Domain.Entities.ValueObjects;
using Domain.Interfaces.Repository.Users;
using System.Text.Json;

namespace Application.Authentication;

public class AuthenticationService(IUserRepository userRepository) : IAuthenticationService
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<Result<Token>> Login(string userName, Password password)
    {
        var user = await _userRepository.GetByUserNameAsync(userName);

        if (!user.Password.Equals(password))
        {
            return Result<Token>.Failure(Errors.LoginFailedError);
        }

        var token = CreateJWTToken(user);
        return Result<Token>.Success(token);
    }
    private Token CreateJWTToken(User user)
    {
        var token = new Token // todo jwt , remove password
        {
            AccessToken = JsonSerializer.Serialize(user),
        };

        return token;
    }
    public Result<CurrentUser> GetCurrentUser(Token token)
    {
        var currentUser = JsonSerializer.Deserialize<CurrentUser>(token.AccessToken);
        return currentUser is null ?
            Result<CurrentUser>.Failure(Errors.LoginFailedError) :
            Result<CurrentUser>.Success(currentUser);
    }
}
