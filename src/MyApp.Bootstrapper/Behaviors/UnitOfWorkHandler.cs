using Bricks;
using MediatR;
using MyApp.Domain;

namespace MyApp.Bootstrapper;
public class UnitOfWorkBehavior<TRequest, TResponse>(MyAppContext context) : IPipelineBehavior<TRequest, TResponse>
{    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.GetType().IsCommand())
            await context.SaveChangesAsync(cancellationToken);

        return response;
    }
}
