using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Application.Exceptions;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Mapster;

namespace CleanTickets.Application.Features.Events.Create;

public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand, EventModel>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventModel> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        Maybe<Event> existingEvent = await _eventRepository.GetAsync(request.Name);

        if (existingEvent.HasValue)
        {
            throw new InvalidRequestException($"An event named {request.Name} already exists");
        }

        Event e = request.Adapt<Event>();

        Event result = await _eventRepository.AddAsync(e);

        return result.Adapt<EventModel>();
    }
}
