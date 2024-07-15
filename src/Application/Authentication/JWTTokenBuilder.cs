using Application.Results;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Authentication;

public class JWTTokenBuilder
{
    private List<Claim> Claims = new();
    private DateTime? ExpiresAt;
    private SigningCredentials signingCredentials;
    public JWTTokenBuilder AddClaim(Claim claim)
    {
        Claims.Add(claim);
        return this;
    }
    public JWTTokenBuilder ExpireAt(int hours)
    {
        ExpiresAt = DateTime.UtcNow.AddHours(hours);
        return this;
    }
    public JWTTokenBuilder WithSigningCredentials(string key, string algorithm)
    {
        var securityKey = Encoding.ASCII.GetBytes(key);
        signingCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), algorithm);
        return this;
    }

    public string Build()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(Claims),
            Expires = ExpiresAt,
            SigningCredentials = signingCredentials,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static async Task<Result<CurrentUser>> ValidateTokenAsync(string token, string key)
    {
        var securityKey = Encoding.ASCII.GetBytes(key);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(securityKey),
            ValidateIssuer = false,
            ValidateAudience = false,
        });

        if (!tokenValidationResult.IsValid || tokenValidationResult.SecurityToken.ValidTo <= DateTime.Now)
            return Result<CurrentUser>.Failure(Errors.TokenIsInvalidError);

        var jwtToken = (JwtSecurityToken)tokenValidationResult.SecurityToken;

        var userNameClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name");

        var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid");

        if (!int.TryParse(userIdClaim?.Value, out int userId))
            return Result<CurrentUser>.Failure(Errors.TokenIsInvalidError);

        var currentUser = new CurrentUser
        {
            userId = userId,
            userName = userNameClaim?.Value ?? ""
        };

        return userId > 0 ? Result<CurrentUser>.Success(currentUser) : Result<CurrentUser>.Failure(Errors.TokenIsInvalidError);

    }
}

