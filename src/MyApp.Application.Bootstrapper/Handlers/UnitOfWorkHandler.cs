using MyApp.Domain.EntityFramework;
using SlickBus;
using System;

namespace MyApp.Application
{
    public class UnitOfWorkHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

        public UnitOfWorkHandler(IUnitOfWork uow, IRequestHandler<TRequest, TResponse> innerHandler)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            _uow = uow;

            if (innerHandler == null)
                throw new ArgumentNullException("innerHandler");

            _innerHandler = innerHandler;
        }

        public TResponse Handle(TRequest message)
        {
            var response = _innerHandler.Handle(message);
            _uow.Commit();

            return response;
        }
    }
}