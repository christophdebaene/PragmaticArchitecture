using Serilog;
using SlickBus;
using System;

namespace MyApp.Application
{
    public class ApplicationExceptionHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

        public ApplicationExceptionHandler(IRequestHandler<TRequest, TResponse> innerHandler)
        {
            if (innerHandler == null)
                throw new ArgumentNullException("innerHandler");

            _innerHandler = innerHandler;
        }

        public TResponse Handle(TRequest message)
        {
            TResponse response;

            try
            {
                response = _innerHandler.Handle(message);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Error in application");
                throw new ApplicationException("Application error", exc);
            }

            return response;
        }
    }
}