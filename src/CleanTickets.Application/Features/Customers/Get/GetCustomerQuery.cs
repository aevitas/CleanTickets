using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Domain;

namespace CleanTickets.Application.Features.Customers.Get;

public record GetCustomerQuery(string FirstName, string LastName) : ICachedQuery<Maybe<CustomerModel>>
{
    public string CacheKey => $"customer.{FirstName}.{LastName}";

    public TimeSpan CacheFor => TimeSpan.FromMinutes(1);
}
