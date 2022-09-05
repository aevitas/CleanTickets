namespace CleanTickets.Application.Contracts;

public record EventModel(string Name, string Location, DateTimeOffset OccursAt, int TicketsAvailable);
