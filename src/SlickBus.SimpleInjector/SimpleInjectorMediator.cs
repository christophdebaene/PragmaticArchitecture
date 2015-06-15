using SimpleInjector;
using System;

namespace SlickBus
{
    public class SimpleInjectorMediator : IMediator
    {
        private readonly Container _container;

        public SimpleInjectorMediator(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType =
                typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic)request);
        }
    }
}