using SimpleInjector;
using SimpleInjector.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SlickBus
{
    public static class ContainerExtensions
    {
        public static void RegisterMediator(this Container container, params Assembly[] assemblies)
        {
            container.Register<IMediator, SimpleInjectorMediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
        }

        public static void RegisterRequestHandlerDecorators(this Container container, IEnumerable<Type> decoratorTypes)
        {
            foreach (var decoratorType in decoratorTypes)
            {
                container.RegisterDecorator(typeof(IRequestHandler<,>), decoratorType);
            }
        }

        public static void RegisterRequestHandlerDecorators(this Container container, IEnumerable<Type> decoratorTypes, Predicate<DecoratorPredicateContext> predicate)
        {
            foreach (var decoratorType in decoratorTypes)
            {
                container.RegisterDecorator(typeof(IRequestHandler<,>), decoratorType, predicate);
            }
        }
    }
}