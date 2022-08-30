using CleanTickets.Application.Exceptions;
using CleanTickets.Application.Features.Events.Create;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Application.Tests.Events;

public class CreateEventTests
{
    [Fact]
    public async Task CreateEvent_Validation()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();

        CreateEventCommand command = new("Homecoming", "Amsterdam", DateTimeOffset.MaxValue, 10000);

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(command with { Name = string.Empty }));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(command with { Location = string.Empty }));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(command with { OccursAt = DateTimeOffset.MinValue }));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(command with { TicketsAvailable = 0 }));
    }

    [Fact]
    public async Task CreateEvent_Created()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();
        IEventRepository eventRepository = services.GetRequiredService<IEventRepository>();

        CreateEventCommand command = new("Great Event", "Paradise", DateTimeOffset.MaxValue, 10);

        await mediator.Send(command);

        Maybe<Event> e = await eventRepository.GetAsync("Great Event");

        Assert.True(e.HasValue);
        Assert.Equal(command.Name, e.Value.Name);
        Assert.Equal(command.Location, e.Value.Location);
        Assert.Equal(command.OccursAt.Date, e.Value.OccursAt.Date);
        Assert.Equal(command.TicketsAvailable, e.Value.TicketsAvailable);
    }
}
