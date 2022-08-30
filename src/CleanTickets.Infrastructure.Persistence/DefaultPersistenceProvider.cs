using CleanTickets.Application.Abstractions;

namespace CleanTickets.Infrastructure.Persistence;

internal class DefaultPersistenceProvider : IPersistenceProvider
{
    private readonly TicketContext _context;

    public DefaultPersistenceProvider(TicketContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
