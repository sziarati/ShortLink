using Application.Results;
using Domain.Entities.ValueObjects;
using Domain.Interfaces.Repository.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly HttpContext _httpContext;
    private readonly Authentications _authenticationConfigs;

    public AuthenticationService(IUserRepository userRepository, IOptions<FeatureConfigurations> options, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContext = httpContextAccessor.HttpContext;
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
    public Result<CurrentUser> GetCurrentUser()
    {
        var token = "";
        token = GetTokenFromHttpContext(token);

        var validateTokenResult = JWTTokenBuilder.ValidateTokenAsync(token, _authenticationConfigs.JWTKey);

        return validateTokenResult.IsSuccess ?
            Result<CurrentUser>.Success(validateTokenResult.Data) :
            Result<CurrentUser>.Failure(Errors.TokenIsInvalidError);
    }
    private string GetTokenFromHttpContext(string token)
    {
        var authorizationHeader = _httpContext.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            token = authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        return token;
    }
}
