using System.Transactions;
using Bricks;
using Mediator;

namespace TodoApp.Infrastructure.Behaviors;
public class TransactionBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    static TransactionOptions s_transactionOptions = new()
    {
        IsolationLevel = IsolationLevel.ReadCommitted,
        Timeout = TransactionManager.MaximumTimeout
    };
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        if (message.GetType().IsCommand())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, s_transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await next(message, cancellationToken);
                scope.Complete();
                return response;
            }
        }
        else
        {
            return await next(message, cancellationToken);
        }
    }
}
