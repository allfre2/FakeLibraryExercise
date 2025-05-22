using Domain.Security;

namespace Application.Services;

public interface IAuthManager
{
    Task<bool> Authenticate(ClientCredentials credentials);
    Task<RegisteredClient> RegisterClient(RegistrationData data);
}
