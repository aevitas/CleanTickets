namespace CleanTickets.Domain.Entities;

#pragma warning disable CS8618

public class Ticket : Entity
{
    public Customer Customer { get; set; }

    public Event Event { get; set; }

    public DateTimeOffset PurchasedAt { get; set; }
}
