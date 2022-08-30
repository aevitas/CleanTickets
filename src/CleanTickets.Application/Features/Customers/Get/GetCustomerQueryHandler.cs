using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Features.Customers.Get;

public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, Maybe<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Maybe<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        Maybe<Customer> result = await _customerRepository.GetByNameAsync(request.FirstName, request.LastName);

        return result;
    }
}
