namespace CleanTickets.Application.Abstractions;

public interface IPersistenceProvider
{
    Task SaveChangesAsync();
}
