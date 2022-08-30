using CleanTickets.Domain.Entities;

namespace CleanTickets.Domain.Abstractions;

public interface ITicketRepository
{
    Task<IReadOnlyList<Ticket>> GetTicketsForEventAsync(long eventId);

    Task<Ticket> AddAsync(Ticket ticket);
}
