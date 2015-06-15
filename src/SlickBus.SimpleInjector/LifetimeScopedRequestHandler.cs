using SimpleInjector;
using System;

namespace SlickBus
{
    public class LifetimeScopedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Container _container;
        private readonly Func<IRequestHandler<TRequest, TResponse>> _innerHandlerFactory;

        public LifetimeScopedRequestHandler(Container container, Func<IRequestHandler<TRequest, TResponse>> innerHandlerFactory)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;

            if (innerHandlerFactory == null)
                throw new ArgumentNullException("innerhandlerFactory");

            _innerHandlerFactory = innerHandlerFactory;
        }

        public TResponse Handle(TRequest message)
        {
            using (_container.BeginLifetimeScope())
            {
                var handler = _innerHandlerFactory();
                var response = handler.Handle(message);
                return response;
            }
        }
    }
}