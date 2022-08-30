using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanTickets.Infrastructure.Persistence.Repositories;

internal class TicketRepository : ITicketRepository
{
    private readonly TicketContext _context;

    public TicketRepository(TicketContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyList<Ticket>> GetTicketsForEventAsync(long eventId)
    {
        List<Ticket> tickets = _context.Tickets.Include(t => t.Event).Where(t => t.Event.Id == eventId).ToList();

        return Task.FromResult((IReadOnlyList<Ticket>)tickets);
    }

    public Task<Ticket> AddAsync(Ticket ticket)
    {
        EntityEntry<Ticket> result = _context.Tickets.Add(ticket);

        return Task.FromResult(result.Entity);
    }
}
