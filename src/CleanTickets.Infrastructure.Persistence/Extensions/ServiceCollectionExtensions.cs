using CleanTickets.Application.Abstractions;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IPersistenceProvider, DefaultPersistenceProvider>();

        return services;
    }
}
