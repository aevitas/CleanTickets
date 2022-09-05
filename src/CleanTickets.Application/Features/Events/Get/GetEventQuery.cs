using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Domain;
using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Features.Events.Get;

public record GetEventQuery(string Name) : ICachedQuery<Maybe<EventModel>>
{
    public string CacheKey => $"event.{Name}";

    public TimeSpan CacheFor => TimeSpan.FromMinutes(1);
}
