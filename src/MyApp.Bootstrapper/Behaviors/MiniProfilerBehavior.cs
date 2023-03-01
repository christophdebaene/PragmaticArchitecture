using MediatR;
using Microsoft.FeatureManagement;
using StackExchange.Profiling;

namespace MyApp.Bootstrapper
{
    public class MiniProfilerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IFeatureManager _featureManager;
        public MiniProfilerBehavior(IFeatureManager featureManager)
        {
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (await _featureManager.IsEnabledAsync("MiniProfiler"))
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
}
