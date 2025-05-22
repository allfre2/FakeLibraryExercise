using Application.Services;
using Domain.Constants;
using System.Security.Cryptography;

namespace Infrastructure.Services;

public class CredentialsGenerator : ICredentialsGenerator
{
    public string GenerateClientId()
    {
        var generator = RandomNumberGenerator.Create();
        byte[] buffer = new byte[Security.ClientIdLength];
        generator.GetBytes(buffer);
        return Convert.ToHexString(buffer);
    }

    public string GenerateClientSecret()
    {
        var generator = RandomNumberGenerator.Create();
        byte[] buffer = new byte[Security.ClientSecretLength];
        generator.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }
}
