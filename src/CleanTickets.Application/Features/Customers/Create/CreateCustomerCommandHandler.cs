using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Application.Exceptions;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Mapster;

namespace CleanTickets.Application.Features.Customers.Create;

internal class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CustomerModel>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerModel> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Maybe<Customer> existingCustomer =
            await _customerRepository.GetByNameAsync(request.FirstName, request.LastName);

        if (existingCustomer.HasValue)
        {
            throw new InvalidRequestException("A customer with that name already exists");
        }

        Customer result =
            await _customerRepository.AddAsync(new Customer
            {
                FirstName = request.FirstName, LastName = request.LastName
            });

        return result.Adapt<CustomerModel>();
    }
}
