using Bricks;
using MediatR;
using TodoApp.Application;

namespace TodoApp.Infrastructure.Behaviors;
public class UnitOfWorkBehavior<TRequest, TResponse>(IApplicationDbContext context) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.GetType().IsCommand())
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        return response;
    }
}
