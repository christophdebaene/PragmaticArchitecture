using System.Diagnostics;
using MediatR;
using Serilog;
using Serilog.Context;
using Serilog.Core;

namespace TodoApp.Infrastructure.Behaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var log = Log.ForContext(Constants.SourceContextPropertyName, request.GetType().FullName);

        using (LogContext.PushProperty("RequestName", request.GetType().Name))
        {
            var elapsedMs = default(double);
            var start = Stopwatch.GetTimestamp();

            TResponse response;

            try
            {
                response = await next();
                elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
            }
            catch (Exception ex)
            {
                log.Error(ex, request.ToString());
                throw;
            }
            finally
            {
                log.Information("Request ended in {Elapsed:0.0000} ms", elapsedMs);
            }

            return response;
        }
    }
    static double GetElapsedMilliseconds(long start, long stop)
    {
        var elapsed = (stop - start) * 1000 / (double)Stopwatch.Frequency;
        return Math.Round(elapsed, 2);
    }
}
