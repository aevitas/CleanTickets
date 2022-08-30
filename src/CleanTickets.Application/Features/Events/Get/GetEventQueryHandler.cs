using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Features.Events.Get;

public class GetEventQueryHandler : IQueryHandler<GetEventQuery, Maybe<Event>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Maybe<Event>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        Maybe<Event> result = await _eventRepository.GetAsync(request.Name);

        return result;
    }
}
