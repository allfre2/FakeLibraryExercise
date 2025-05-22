namespace Application.Services;

public interface ICredentialsGenerator
{
    public string GenerateClientId();
    public string GenerateClientSecret();
}
