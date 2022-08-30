using FluentValidation;

namespace CleanTickets.Application.Features.Events.Get;

internal class GetEventValidator : AbstractValidator<GetEventQuery>
{
    public GetEventValidator()
    {
        RuleFor(q => q.Name).NotEmpty();
    }
}
