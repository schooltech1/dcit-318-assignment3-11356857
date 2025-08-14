using System;

public class MissingFieldException : Exception
{
    public MissingFieldException(string message) : base(message) { }
}
