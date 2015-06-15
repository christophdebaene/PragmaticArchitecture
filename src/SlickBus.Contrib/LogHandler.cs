using Serilog;
using Serilog.Context;
using System;
using System.Diagnostics;

namespace SlickBus
{
    public class LogHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

        public LogHandler(IRequestHandler<TRequest, TResponse> innerHandler)
        {
            if (innerHandler == null)
                throw new ArgumentNullException("innerHandler");

            _innerHandler = innerHandler;
        }

        public TResponse Handle(TRequest request)
        {
            using (LogContext.PushProperty("RequestId", request.Id))
            {
                Log.Information("Request {RequestName} started", request.GetType().Name);

                var stopwatch = Stopwatch.StartNew();
                var response = _innerHandler.Handle(request);
                stopwatch.Stop();

                Log.Information("Request {RequestName} ended in {Elapsed:000000} ms", request.GetType().Name, stopwatch.ElapsedMilliseconds);

                return response;
            }
        }
    }
}