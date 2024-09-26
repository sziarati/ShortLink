using Application.Results;
using Domain.Entities.ValueObjects;
using Domain.Interfaces.Repository.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly Authentications _authenticationConfigs;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        IUserRepository userRepository, 
        IOptions<Authentications> options,
        ILogger<AuthenticationService> logger)
    {
        _userRepository = userRepository;
        _authenticationConfigs = options.Value;
        _logger = logger;
    }

    public async Task<Result<string>> Login(string userName, Password password)
    {
        var user = await _userRepository.GetByUserNameAsync(userName);
        
        if (user is null || !user.Password.Equals(password))
        {
            _logger.LogInformation("user {0} failed to log in.", userName);
            return Result<string>.Failure(Errors.LoginFailedError);
        }

        var token = new JWTTokenBuilder()
            .WithSigningCredentials(_authenticationConfigs.JWTKey, SecurityAlgorithms.HmacSha256Signature)
            .ExpireAt(_authenticationConfigs.Timeout)
            .AddClaim(new Claim(ClaimTypes.Name, user.UserName))
            .AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()))
            .Build();

        _logger.LogInformation("user {0} logged in successfully.", userName);
        return Result<string>.Success(token);
    }
}
