using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanTickets.Infrastructure.Persistence.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    private readonly TicketContext _context;

    public CustomerRepository(TicketContext context)
    {
        _context = context;
    }

    public Task<Customer> AddAsync(Customer customer)
    {
        EntityEntry<Customer> result = _context.Customers.Add(customer);

        return Task.FromResult(result.Entity);
    }

    public Task<Maybe<Customer>> GetByNameAsync(string firstName, string lastName)
    {
        Customer? result = _context.Customers.FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName);

        return Task.FromResult(Maybe.Wrap(result));
    }

    public Task<Maybe<Customer>> FindAsync(long id)
    {
        Customer? result = _context.Customers.Find(id);

        return Task.FromResult(Maybe.Wrap(result));
    }
}
