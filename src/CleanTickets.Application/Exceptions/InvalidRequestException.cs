namespace CleanTickets.Application.Exceptions;

public class InvalidRequestException : Exception
{
    internal InvalidRequestException(string? message) : base(message)
    {
    }
}
