using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Model;
using MyApp.Domain.Todos;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.ReadModel.Todos
{
    public record GetTodoDetail : IRequest<TodoDetailModel>
    {
        public Guid TodoId { get; init; }        
    }
    public class GetTodoDetailHandler: IRequestHandler<GetTodoDetail, TodoDetailModel>
    {
        private readonly MyAppContext _context;
        public GetTodoDetailHandler(MyAppContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _context = context;
        }    
        public async Task<TodoDetailModel> Handle(GetTodoDetail query, CancellationToken cancellationToken)
        {
            return await _context.Set<Todo>()
              .AsNoTracking()
              .Select(x => new TodoDetailModel
              {
                  Id = x.Id,
                  Title = x.Title,
                  Priority = (int)x.Priority
              })
              .SingleOrDefaultAsync(x => x.Id == query.TodoId, cancellationToken);
        }
    }
}