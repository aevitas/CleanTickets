using CleanTickets.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanTickets.Application.Behaviors;

internal class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        ValidationContext<TRequest> ctx = new(request);
        List<ValidationResult> results = new();

        foreach (IValidator<TRequest> v in _validators)
        {
            results.Add(await v.ValidateAsync(ctx, cancellationToken));
        }

        List<ValidationResult> validationFailures = results.Where(r => !r.IsValid).ToList();

        if (!validationFailures.Any())
        {
            return await next();
        }

        List<ValidationFailure> errors = new();

        foreach (ValidationResult e in validationFailures)
        {
            errors.AddRange(e.Errors);
        }

        throw new RequestValidationException(errors);
    }
}
