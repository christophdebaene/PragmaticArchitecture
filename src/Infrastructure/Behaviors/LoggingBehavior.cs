using System.Diagnostics;
using Mediator;
using Serilog;
using Serilog.Context;
using Serilog.Core;

namespace TodoApp.Infrastructure.Behaviors;
public class LoggingBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        var log = Log.ForContext(Constants.SourceContextPropertyName, message.GetType().FullName);

        using (LogContext.PushProperty("RequestName", message.GetType().Name))
        {
            var elapsedMs = default(double);
            var start = Stopwatch.GetTimestamp();

            TResponse response;

            try
            {
                response = await next(message, cancellationToken);
                elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
            }
            catch (Exception ex)
            {
                log.Error(ex, message.ToString());
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
