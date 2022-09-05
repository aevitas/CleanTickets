using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Features.Events.Create;

public record CreateEventCommand
    (string Name, string Location, DateTimeOffset OccursAt, int TicketsAvailable) : ICommand<EventModel>;
