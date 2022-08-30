using FluentValidation;

namespace CleanTickets.Application.Features.Events.Create;

internal class CreateEventValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Location).NotEmpty();
        RuleFor(c => c.OccursAt).Must(offset => offset > DateTimeOffset.UtcNow)
            .WithMessage("Event occurrence must be in the future");
        RuleFor(c => c.TicketsAvailable).Must(i => i > 0).WithMessage("Event must have at least one ticket available");
    }
}
