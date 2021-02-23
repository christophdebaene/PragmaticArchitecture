using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Bricks;
using MediatR;

namespace MyApp.Application.Bootstrapper
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        static TransactionOptions s_transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        };
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request.GetType().IsCommand())
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, s_transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var response = await next();
                    scope.Complete();
                    return response;
                }
            }
            else
            {
                return await next();
            }
        }
    }
}
