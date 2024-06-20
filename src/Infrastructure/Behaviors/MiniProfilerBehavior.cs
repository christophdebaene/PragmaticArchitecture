using MediatR;
using Microsoft.FeatureManagement;
using StackExchange.Profiling;

namespace TodoApp.Infrastructure.Behaviors;
public class MiniProfilerBehavior<TRequest, TResponse>(IFeatureManager featureManager) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (await featureManager.IsEnabledAsync("MiniProfiler"))
        {
            var requestName = request.GetType().Name;

            using (MiniProfiler.Current.CustomTiming(requestName, string.Empty))
            {
                return await next();
            }
        }
        else
        {
            return await next();
        }
    }
}
