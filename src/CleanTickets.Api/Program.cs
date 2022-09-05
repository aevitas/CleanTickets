using CleanTickets.Application.Contracts;
using CleanTickets.Application.Extensions;
using CleanTickets.Application.Features.Customers.Create;
using CleanTickets.Application.Features.Customers.Get;
using CleanTickets.Application.Features.Events.Create;
using CleanTickets.Application.Features.Events.Get;
using CleanTickets.Application.Features.Tickets.Book;
using CleanTickets.Domain;
using CleanTickets.Infrastructure.Persistence;
using CleanTickets.Infrastructure.Persistence.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationCore();
builder.Services.AddValidators();
builder.Services.AddRepositories();

builder.Services.AddDbContext<TicketContext>(opts => opts.UseInMemoryDatabase("Api"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/events/{name}", async (IMediator mediator, string name) =>
{
    Maybe<EventModel> result = await mediator.Send(new GetEventQuery(name));

    return result.HasValue ? Results.Ok(result.Value) : Results.NotFound();
}).WithName("GetEvent");

app.MapPost("/events", async (IMediator mediator, [FromBody] CreateEventCommand command) =>
{
    EventModel result;
    try
    {
        result = await mediator.Send(command);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }

    return Results.Ok(result);
});

app.MapGet("customers/find", async (IMediator mediator, string firstName, string lastName) =>
{
    Maybe<CustomerModel> result = await mediator.Send(new GetCustomerQuery(firstName, lastName));

    return result.HasValue ? Results.Ok(result.Value) : Results.NotFound();
});

app.MapPost("customers", async (IMediator mediator, [FromBody] CreateCustomerCommand command) =>
{
    CustomerModel result;
    try
    {
        result = await mediator.Send(command);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }

    return Results.Ok(result);
});

app.MapPost("bookings/create", async (IMediator mediator, [FromBody] BookTicketsCommand command) =>
{
    BookingResult result;
    try
    {
        result = await mediator.Send(command);
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }

    return Results.Ok(result);
});

app.Run();
