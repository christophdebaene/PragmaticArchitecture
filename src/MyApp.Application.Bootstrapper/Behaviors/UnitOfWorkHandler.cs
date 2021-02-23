using System;
using System.Threading;
using System.Threading.Tasks;
using Bricks;
using MediatR;
using MyApp.Domain;

namespace MyApp.Application.Bootstrapper
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly MyAppContext _context;
        public UnitOfWorkBehavior(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request.GetType().IsCommand())
            {
                var response = await next();
                await _context.SaveChangesAsync(cancellationToken);
                return response;
            }
            else
            {
                return await next();
            }
        }
    }
}
