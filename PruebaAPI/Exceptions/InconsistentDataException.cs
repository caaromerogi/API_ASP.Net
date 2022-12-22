namespace PruebaAPI.Exceptions;

public class InconsistentDataException : Exception
{
    public InconsistentDataException(string? message) : base(message)
    {
    }
}