using Domain.Security;

namespace Application.Services;

public interface ITokenGeneratorService
{
    public Task<AccessToken> GenerateAccessToken(ClientCredentials credentials);
}
