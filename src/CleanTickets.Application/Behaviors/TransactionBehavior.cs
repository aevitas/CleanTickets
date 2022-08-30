using System.Transactions;
using CleanTickets.Application.Abstractions;
using MediatR;

namespace CleanTickets.Application.Behaviors;

internal class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IPersistenceProvider _persistenceProvider;

    public TransactionBehavior(IPersistenceProvider persistenceProvider)
    {
        _persistenceProvider = persistenceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        using TransactionScope transaction = new(TransactionScopeAsyncFlowOption.Enabled);

        TResponse response = await next();

        try
        {
            await _persistenceProvider.SaveChangesAsync();
        }
        catch (Exception)
        {
            transaction.Dispose();

            throw;
        }

        transaction.Complete();

        return response;
    }
}
