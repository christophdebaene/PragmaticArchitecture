using MediatR;
using MyApp.Application.Commands;
using MyApp.Domain.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers
{
    public class TaskHandlers :

        IRequestHandler<CreateNewTask, Unit>,
        IRequestHandler<CompleteTodo, Unit>,
        IRequestHandler<IncreasePriority, Unit>,
        IRequestHandler<DecreasePriority, Unit>,
        IRequestHandler<SetTaskDueDate, Unit>,
        IRequestHandler<DeleteTask, Unit>
    {
        private readonly ISystemClock _systemClock;
        private readonly MyAppContext _context;
        public TaskHandlers(MyAppContext context, ISystemClock systemClock)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _systemClock = systemClock ?? throw new ArgumentNullException(nameof(systemClock));
        }
        public async Task<Unit> Handle(CreateNewTask command, CancellationToken cancellationToken)
        {
            var task = new Todo(command.TodoId, command.Title, _systemClock);
            await _context.AddAsync(task);
            return Unit.Value;
        }
        public async Task<Unit> Handle(CompleteTodo command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            todo.Complete();

            return Unit.Value;
        }
        public async Task<Unit> Handle(IncreasePriority command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            todo.IncreasePriority();

            return Unit.Value;
        }
        public async Task<Unit> Handle(DecreasePriority command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            todo.DecreasePriority();

            return Unit.Value;
        }
        public async Task<Unit> Handle(SetTaskDueDate command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            todo.SetDueDate(command.DueDate);

            return Unit.Value;
        }
        public async Task<Unit> Handle(DeleteTask command, CancellationToken cancellationToken)
        {
            var todo = await _context.Set<Todo>().FindAsync(command.TodoId);
            _context.Set<Todo>().Remove(todo);

            return Unit.Value;
        }
    }
}