using CleanTickets.Domain.Entities;

namespace CleanTickets.Domain.Abstractions;

public interface ICustomerRepository
{
    Task<Customer> AddAsync(Customer customer);

    Task<Maybe<Customer>> GetByNameAsync(string firstName, string lastName);

    Task<Maybe<Customer>> FindAsync(long id);
}
