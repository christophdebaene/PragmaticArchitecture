using System;
using System.Transactions;

namespace SlickBus
{
    public class TransactionHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

        public TransactionHandler(IRequestHandler<TRequest, TResponse> innerHandler)
        {
            if (innerHandler == null)
                throw new ArgumentNullException("innerHandler");

            _innerHandler = innerHandler;
        }

        public TResponse Handle(TRequest message)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var response = _innerHandler.Handle(message);
                scope.Complete();

                return response;
            }
        }
    }
}