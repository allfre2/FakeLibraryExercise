﻿namespace Domain.Exceptions;

public class RoleAdditionException : Exception
{
    public RoleAdditionException(string msg) : base(msg) { }
}
