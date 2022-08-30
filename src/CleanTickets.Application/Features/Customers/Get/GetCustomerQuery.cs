using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Domain;
using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Features.Customers.Get;

public record GetCustomerQuery(string FirstName, string LastName) : ICachedQuery<Maybe<Customer>>
{
    public string CacheKey => $"customer.{FirstName}.{LastName}";

    public TimeSpan CacheFor => TimeSpan.FromMinutes(1);
}