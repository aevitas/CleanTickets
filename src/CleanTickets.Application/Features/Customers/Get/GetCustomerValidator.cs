using FluentValidation;

namespace CleanTickets.Application.Features.Customers.Get;

internal class GetCustomerValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerValidator()
    {
        RuleFor(q => q.FirstName).NotEmpty();
        RuleFor(q => q.LastName).NotEmpty();
    }
}
