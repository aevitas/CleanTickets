using CleanTickets.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
}
