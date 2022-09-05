using CleanTickets.Application.Contracts;
using CleanTickets.Application.Exceptions;
using CleanTickets.Application.Features.Customers.Get;
using CleanTickets.Domain;
using CleanTickets.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Application.Tests.Customers;

public class GetCustomerTests
{
    [Fact]
    public async Task GetCustomer_Validation()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(new GetCustomerQuery(string.Empty, "Bone")));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(new GetCustomerQuery("Ken", string.Empty)));
    }

    [Fact]
    public async Task GetCustomer_Found()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider(ctx =>
        {
            ctx.Customers.Add(new Customer { FirstName = "Ken", LastName = "Bone" });
        });
        IMediator mediator = services.GetRequiredService<IMediator>();

        Maybe<CustomerModel> found = await mediator.Send(new GetCustomerQuery("Ken", "Bone"));
        Maybe<CustomerModel> notFound = await mediator.Send(new GetCustomerQuery("Hugh", "Mungus"));

        Assert.True(found.HasValue);
        Assert.False(notFound.HasValue);

        Assert.Equal("Ken", found.Value.FirstName);
        Assert.Equal("Bone", found.Value.LastName);
    }
}
