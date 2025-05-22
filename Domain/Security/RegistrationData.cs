namespace Domain.Security;

public class RegistrationData
{
    public string Email { get; set; }
    public string Application { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public IDictionary<string, string>? Claims { get; set; }
}
