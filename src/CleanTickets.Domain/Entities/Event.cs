namespace CleanTickets.Domain.Entities;

#pragma warning disable CS8618

public class Event : Entity
{
    public string Name { get; set; }

    public string Location { get; set; }

    public DateTimeOffset OccursAt { get; set; }

    public int TicketsAvailable { get; set; }
}
