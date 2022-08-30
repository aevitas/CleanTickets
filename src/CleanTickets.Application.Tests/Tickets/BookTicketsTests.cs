using CleanTickets.Application.Exceptions;
using CleanTickets.Application.Features.Tickets.Book;
using CleanTickets.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Application.Tests.Tickets;

public class BookTicketsTests
{
    [Fact]
    public async Task BookTickets_Validation()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider(ctx =>
        {
            ctx.Customers.Add(new Customer { Id = 1, FirstName = "Ken", LastName = "Bone" });
            ctx.Events.Add(new Event
            {
                Id = 1,
                Location = "Ohio",
                Name = "Homecoming",
                OccursAt = DateTimeOffset.UtcNow.AddDays(30),
                TicketsAvailable = 5000
            });
        });
        IMediator mediator = services.GetRequiredService<IMediator>();

        BookTicketsCommand command = new(1, 1, 10);

        // Disallow default values
        await Assert.ThrowsAsync<RequestValidationException>(() => mediator.Send(command with { EventId = 0 }));
        await Assert.ThrowsAsync<RequestValidationException>(() => mediator.Send(command with { CustomerId = 0 }));
        await Assert.ThrowsAsync<RequestValidationException>(() => mediator.Send(command with { TicketCount = 0 }));

        // Make sure non-existent customers and events fail validation
        await Assert.ThrowsAsync<RequestValidationException>(() => mediator.Send(command with { EventId = 50 }));
        await Assert.ThrowsAsync<RequestValidationException>(() => mediator.Send(command with { CustomerId = 50 }));
    }

    [Fact]
    public async Task BookTickets_AtCapacity()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider(ctx =>
        {
            Customer customer = new() { Id = 1, FirstName = "Ken", LastName = "Bone" };
            Event e = new()
            {
                Id = 1,
                Location = "Ohio",
                Name = "Homecoming",
                OccursAt = DateTimeOffset.UtcNow.AddDays(30),
                TicketsAvailable = 10
            };

            ctx.Customers.Add(customer);
            ctx.Events.Add(e);

            ctx.Tickets.AddRange(Enumerable.Range(0, 10).Select(_ =>
                new Ticket { Customer = customer, Event = e, PurchasedAt = DateTimeOffset.UtcNow }));
        });
        IMediator mediator = services.GetRequiredService<IMediator>();

        await Assert.ThrowsAsync<InvalidRequestException>(() => mediator.Send(new BookTicketsCommand(1, 1, 5)));
    }

    [Fact]
    public async Task BookTickets_InsufficientAvailable()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider(ctx =>
        {
            Customer customer = new() { Id = 1, FirstName = "Ken", LastName = "Bone" };
            Event e = new()
            {
                Id = 1,
                Location = "Ohio",
                Name = "Homecoming",
                OccursAt = DateTimeOffset.UtcNow.AddDays(30),
                TicketsAvailable = 15
            };

            ctx.Customers.Add(customer);
            ctx.Events.Add(e);

            ctx.Tickets.AddRange(Enumerable.Range(0, 10).Select(_ =>
                new Ticket { Customer = customer, Event = e, PurchasedAt = DateTimeOffset.UtcNow }));
        });
        IMediator mediator = services.GetRequiredService<IMediator>();

        await Assert.ThrowsAsync<InvalidRequestException>(() => mediator.Send(new BookTicketsCommand(1, 1, 10)));
    }

    [Fact]
    public async Task BookTickets_ValidBooking()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider(ctx =>
        {
            Customer customer = new() { Id = 1, FirstName = "Ken", LastName = "Bone" };
            Event e = new()
            {
                Id = 1,
                Location = "Ohio",
                Name = "Homecoming",
                OccursAt = DateTimeOffset.UtcNow.AddDays(30),
                TicketsAvailable = 500
            };

            ctx.Customers.Add(customer);
            ctx.Events.Add(e);

            ctx.Tickets.AddRange(Enumerable.Range(0, 10).Select(_ =>
                new Ticket { Customer = customer, Event = e, PurchasedAt = DateTimeOffset.UtcNow }));
        });
        IMediator mediator = services.GetRequiredService<IMediator>();

        BookingResult tickets = await mediator.Send(new BookTicketsCommand(1, 1, 10));

        Assert.NotNull(tickets);
        Assert.Equal(10, tickets.Tickets.Count);
        Assert.True(tickets.Tickets.All(t => t.Customer.FirstName == "Ken"));
        Assert.True(tickets.Tickets.All(t => t.Customer.LastName == "Bone"));
        Assert.True(tickets.Tickets.All(t => t.Event.Name == "Homecoming"));
    }
}
