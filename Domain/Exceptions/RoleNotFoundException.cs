namespace Domain.Exceptions;

public class RoleNotFoundException : Exception
{
    public RoleNotFoundException(string msg) : base(msg) { }
}
