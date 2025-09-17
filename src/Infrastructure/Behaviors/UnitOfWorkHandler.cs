using Bricks;
using Mediator;
using TodoApp.Application;

namespace TodoApp.Infrastructure.Behaviors;
public class UnitOfWorkBehavior<TMessage, TResponse>(IApplicationDbContext context) : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next(message, cancellationToken);

        if (message.GetType().IsCommand())
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        return response;
    }
}
