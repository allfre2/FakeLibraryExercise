using Application.Services;
using Domain.Exceptions;
using Domain.Security;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Services;

public class AuthManager : IAuthManager
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ICredentialsGenerator _credentialsGenerator;

    public AuthManager(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ICredentialsGenerator credentialsGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _credentialsGenerator = credentialsGenerator;
    }

    public async Task<bool> Authenticate(ClientCredentials credentials)
    {
        var identityUser = await _userManager.FindByNameAsync(credentials.ClientId);

        if (identityUser is null) return false;

        return await _userManager.CheckPasswordAsync(identityUser, credentials.ClientSecret);
    }

    public async Task<RegisteredClient> RegisterClient(RegistrationData data)
    {
        var roles = await ValidateRoles(data.Roles);
        
        var clientId = _credentialsGenerator.GenerateClientId();
        var clientSecret = _credentialsGenerator.GenerateClientSecret();

        var identityUser = new IdentityUser
        {
            UserName = clientId,
            Email = data.Email,
        };

        var result = await _userManager.CreateAsync(identityUser, clientSecret);

        if (!result.Succeeded) throw new RegistrationException(result.ToString());

        var claimsResult = await _userManager.AddClaimsAsync(
            identityUser,
            data.Claims?.Select(pair => new Claim(pair.Key, pair.Value)) ?? new List<Claim>()
        );

        if (!claimsResult.Succeeded) throw new ClaimsAdditionException(result.ToString());

        var rolesResult = await _userManager.AddToRolesAsync(identityUser, roles);

        if (!rolesResult.Succeeded) throw new RoleAdditionException(result.ToString());

        return new RegisteredClient
        {
            Email = data.Email,
            ClientId = identityUser.UserName,
            ClientSecret = clientSecret,
            Roles = roles,
            Claims = data.Claims,
            CreatedOn = DateTime.Now
        };
    }

    #region Private Methods

    private async Task<IEnumerable<string>> ValidateRoles(IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            if (await _roleManager.FindByNameAsync(role) is null)
            {
                throw new RoleNotFoundException($"El rol: {role} no existe.");
            }
        }

        return roles;
    }

    #endregion
}
