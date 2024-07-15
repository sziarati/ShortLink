using Application.Results;
using Domain.Entities.ValueObjects;
using Domain.Interfaces.Repository.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly Authentications _authenticationConfigs;

    public AuthenticationService(IUserRepository userRepository, IOptions<FeatureConfigurations> options)
    {
        _userRepository = userRepository;
        _authenticationConfigs = options.Value.Authentications;
    }

    public async Task<Result<string>> Login(string userName, Password password)
    {
        var user = await _userRepository.GetByUserNameAsync(userName);
        
        if (user is null || !user.Password.Equals(password))
        {
            return Result<string>.Failure(Errors.LoginFailedError);
        }

        var token = new JWTTokenBuilder()
            .WithSigningCredentials(_authenticationConfigs.JWTKey, SecurityAlgorithms.HmacSha256Signature)
            .ExpireAt(_authenticationConfigs.Timeout)
            .AddClaim(new Claim(ClaimTypes.Name, user.UserName))
            .AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()))
            .Build();

        return Result<string>.Success(token);
    }
}
