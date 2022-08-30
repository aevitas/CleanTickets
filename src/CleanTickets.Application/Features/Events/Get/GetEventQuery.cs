using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Domain;
using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Features.Events.Get;

public record GetEventQuery(string Name) : ICachedQuery<Maybe<Event>>
{
    public string CacheKey => $"event.{Name}";

    public TimeSpan CacheFor => TimeSpan.FromMinutes(1);
}
