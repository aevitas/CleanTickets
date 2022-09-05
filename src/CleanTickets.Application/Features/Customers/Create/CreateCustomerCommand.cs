using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;

namespace CleanTickets.Application.Features.Customers.Create;

public record CreateCustomerCommand(string FirstName, string LastName) : ICommand<CustomerModel>;
