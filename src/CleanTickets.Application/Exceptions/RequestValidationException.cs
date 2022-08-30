using FluentValidation;
using FluentValidation.Results;

namespace CleanTickets.Application.Exceptions;

public class RequestValidationException : ValidationException
{
    internal RequestValidationException(IEnumerable<ValidationFailure> validationFailures) : base(validationFailures)
    {
    }
}
