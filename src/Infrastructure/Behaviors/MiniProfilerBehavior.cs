using Mediator;
using Microsoft.FeatureManagement;
using StackExchange.Profiling;

namespace TodoApp.Infrastructure.Behaviors;
public class MiniProfilerBehavior<TMessage, TResponse>(IFeatureManager featureManager) : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if (await featureManager.IsEnabledAsync("MiniProfiler"))
        {
            var requestName = message.GetType().Name;

            using (MiniProfiler.Current.CustomTiming(requestName, string.Empty))
            {
                return await next(message, cancellationToken);
            }
        }
        else
        {
            return await next(message, cancellationToken);
        }
    }
}
