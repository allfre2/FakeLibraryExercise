namespace Domain.Exceptions;

public class RegistrationException : Exception
{
    public RegistrationException(string msg) : base(msg) { }
}
