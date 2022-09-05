using CleanTickets.Application.Contracts;
using CleanTickets.Application.Exceptions;
using CleanTickets.Application.Features.Events.Get;
using CleanTickets.Domain;
using CleanTickets.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Application.Tests.Events;

public class GetEventTests
{
    [Fact]
    public async Task GetEvent_Validation()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();

        await Assert.ThrowsAsync<RequestValidationException>(() => mediator.Send(new GetEventQuery(string.Empty)));
    }

    [Fact]
    public async Task GetEvent_Found()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider(ctx =>
        {
            ctx.Events.Add(new Event
            {
                Location = "Test", Name = "Event", OccursAt = DateTimeOffset.MaxValue, TicketsAvailable = 100
            });
        });

        IMediator mediator = services.GetRequiredService<IMediator>();

        Maybe<EventModel> found = await mediator.Send(new GetEventQuery("Event"));
        Maybe<EventModel> notFound = await mediator.Send(new GetEventQuery("Woodstock"));

        Assert.True(found.HasValue);
        Assert.False(notFound.HasValue);
    }
}
