using CleanTickets.Application.Exceptions;
using CleanTickets.Application.Features.Customers.Create;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Application.Tests.Customers;

public class CreateCustomerTests
{
    [Fact]
    public async Task CreateCustomer_Validation()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();

        CreateCustomerCommand command = new("Ken", "Bone");

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(command with { FirstName = string.Empty }));

        await Assert.ThrowsAsync<RequestValidationException>(() =>
            mediator.Send(command with { LastName = string.Empty }));
    }

    [Fact]
    public async Task CreateCustomer_Created()
    {
        IServiceProvider services = Services.CreateDefaultServiceProvider();
        IMediator mediator = services.GetRequiredService<IMediator>();
        ICustomerRepository repository = services.GetRequiredService<ICustomerRepository>();

        CreateCustomerCommand command = new("Ken", "Bone");

        await mediator.Send(command);

        Maybe<Customer> customer = await repository.GetByNameAsync(command.FirstName, command.LastName);

        Assert.True(customer.HasValue);
        Assert.Equal(command.FirstName, customer.Value.FirstName);
        Assert.Equal(command.LastName, customer.Value.LastName);
    }
}
