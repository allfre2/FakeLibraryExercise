using Application.Services;
using Domain.Security;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly JwtConfiguration _jwtConfig;
    private readonly UserManager<IdentityUser> _userManager;

    public TokenGeneratorService(
        IOptionsMonitor<JwtConfiguration> jwtConfig,
        UserManager<IdentityUser> userManager
    )
    {
        _jwtConfig = jwtConfig.CurrentValue;
        _userManager = userManager;
    }

    public async Task<AccessToken> GenerateAccessToken(ClientCredentials credentials)
    {
        var accessToken = new AccessToken
        {
            Token = await GenerateToken(credentials),
            Type = "Bearer",
            Expires = TimeSpan.FromMinutes(_jwtConfig.TokenExpirationTime)
        };

        return accessToken;
    }

    public async Task<string> GenerateToken(ClientCredentials credentials)
    {
        var identityUser = await _userManager.FindByNameAsync(credentials.ClientId);
        var userClaims = await _userManager.GetClaimsAsync(identityUser);
        var roles = await _userManager.GetRolesAsync(identityUser);

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, identityUser.UserName) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        claims.AddRange(userClaims);

        var token = new JwtSecurityTokenHandler()
            .WriteToken(new JwtSecurityToken(
                claims: claims,
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.Now.AddMinutes(_jwtConfig.TokenExpirationTime),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret)),
                    SecurityAlgorithms.HmacSha512Signature
                )
            ));

        return token;
    }
}
