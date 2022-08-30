using CleanTickets.Domain.Entities;

namespace CleanTickets.Domain.Abstractions;

public interface IEventRepository
{
    Task<Maybe<Event>> GetAsync(string name);

    Task<Event> AddAsync(Event e);

    Task<Maybe<Event>> FindAsync(long id);
}
