using CleanTickets.Application.Exceptions;
using CleanTickets.Application.Features.Events.Create;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Application.Tests;

public class CreateEventTests
{
    [Fact]
    public async Task CreateEvent_Validation()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(new CreateEventCommand(string.Empty, "Amsterdam", DateTimeOffset.MaxValue, 10000)));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(new CreateEventCommand("Homecoming", string.Empty, DateTimeOffset.MaxValue, 10000)));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(new CreateEventCommand("Homecoming", "Amsterdam", DateTimeOffset.MinValue, 10000)));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(new CreateEventCommand("Homecoming", "Amsterdam", DateTimeOffset.MaxValue, 0)));
    }

    [Fact]
    public async Task CreateEvent_Created()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();
        IEventRepository eventRepository = services.GetRequiredService<IEventRepository>();

        await mediator.Send(new CreateEventCommand("Great Event", "Paradise", DateTimeOffset.MaxValue, 10));

        Maybe<Event> e = await eventRepository.GetAsync("Great Event");

        Assert.True(e.HasValue);
        Assert.Equal("Great Event", e.Value.Name);
        Assert.Equal("Paradise", e.Value.Location);
        Assert.Equal(DateTime.MaxValue.Date, e.Value.OccursAt.Date);
        Assert.Equal(10, e.Value.TicketsAvailable);
    }
}
