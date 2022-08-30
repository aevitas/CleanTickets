using CleanTickets.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CleanTickets.Application.Behaviors;

internal class QueryCachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery<TResponse>
{
    private readonly IMemoryCache _memoryCache;

    public QueryCachingBehaviour(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        string cacheKey = request.CacheKey;

        if (string.IsNullOrWhiteSpace(cacheKey))
            throw new ArgumentException("Could not determine the cache key for the specified query");

        if (_memoryCache.TryGetValue(cacheKey, out TResponse response))
            return response;

        TResponse result = await next();

        _memoryCache.Set(cacheKey, result, request.CacheFor);

        return result;
    }
}

