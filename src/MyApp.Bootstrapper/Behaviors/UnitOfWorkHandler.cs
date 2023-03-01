using Bricks;
using MediatR;
using MyApp.Domain;

namespace MyApp.Bootstrapper
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly MyAppContext _context;
        public UnitOfWorkBehavior(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (request.GetType().IsCommand())
                await _context.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
