using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanTickets.Infrastructure.Persistence.Repositories;

internal class EventRepository : IEventRepository
{
    private readonly TicketContext _context;

    public EventRepository(TicketContext context)
    {
        _context = context;
    }

    public Task<Maybe<Event>> GetAsync(string name)
    {
        Event? result = _context.Events.FirstOrDefault(e => e.Name == name);

        return Task.FromResult(Maybe.Wrap(result));
    }

    public Task<Event> AddAsync(Event e)
    {
        EntityEntry<Event> result = _context.Events.Add(e);

        return Task.FromResult(result.Entity);
    }
}
