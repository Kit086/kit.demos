namespace AAADemo.Domain.Exceptions;

public class UnsupportedColorException : Exception
{
    public UnsupportedColorException(string code) : base($"Color \"{code}\" is unsupported.")
    {
    }
}