using CleanTickets.Domain;
using CleanTickets.Domain.Entities;
using FlakeId;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

#pragma warning disable CS8618

namespace CleanTickets.Infrastructure.Persistence;

public class TicketContext : DbContext
{
    public TicketContext(DbContextOptions options) : base(options)
    {
    }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    internal DbSet<Customer> Customers { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.Id == default)
                        entry.Entity.Id = Id.Create();
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}
