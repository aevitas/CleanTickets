using CleanTickets.Application.Extensions;
using CleanTickets.Infrastructure.Persistence;
using CleanTickets.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTickets.Application.Tests;

internal static class Services
{
    public static IServiceProvider CreateDefaultServiceProvider(Action<TicketContext>? contextInitializer = null)
    {
        ServiceCollection services = new();

        services.AddApplicationCore();
        services.AddValidators();
        services.AddRepositories();

        DbContextOptions options = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        TicketContext context = new(options);

        services.AddScoped(_ => context);

        if (contextInitializer != null)
        {
            contextInitializer(context);

            context.SaveChanges();
        }

        return services.BuildServiceProvider();
    }
}
