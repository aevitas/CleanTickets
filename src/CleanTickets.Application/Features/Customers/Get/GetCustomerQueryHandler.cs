using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Mapster;

namespace CleanTickets.Application.Features.Customers.Get;

public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, Maybe<CustomerModel>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Maybe<CustomerModel>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        Maybe<Customer> result = await _customerRepository.GetByNameAsync(request.FirstName, request.LastName);

        return result.HasValue ? Maybe.Of(result.Value.Adapt<CustomerModel>()) : Maybe<CustomerModel>.NoValue;
    }
}
