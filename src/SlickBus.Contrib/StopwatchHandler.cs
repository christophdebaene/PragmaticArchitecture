using Serilog;
using System;
using System.Diagnostics;

namespace SlickBus
{
    public class StopwatchHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

        public StopwatchHandler(IRequestHandler<TRequest, TResponse> innerHandler)
        {
            if (innerHandler == null)
                throw new ArgumentNullException("innerHandler");

            _innerHandler = innerHandler;
        }

        public TResponse Handle(TRequest message)
        {
            Log.Information("Request {RequestName} started", message.GetType().Name);

            var stopwatch = Stopwatch.StartNew();
            var response = _innerHandler.Handle(message);
            stopwatch.Stop();

            Log.Information("Request {RequestName} ended in {Elapsed:000000} ms", message.GetType().Name, stopwatch.ElapsedMilliseconds);

            return response;
        }
    }
}