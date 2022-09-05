using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Mapster;

namespace CleanTickets.Application.Features.Events.Get;

public class GetEventQueryHandler : IQueryHandler<GetEventQuery, Maybe<EventModel>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Maybe<EventModel>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        Maybe<Event> result = await _eventRepository.GetAsync(request.Name);

        return result.HasValue ? Maybe.Of(result.Value.Adapt<EventModel>()) : Maybe<EventModel>.NoValue;
    }
}
