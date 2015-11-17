using MyApp.Application.Commands;
using MyApp.Domain.Model;
using MyApp.Domain.Repositories;
using SlickBus;
using System;

namespace MyApp.Application.Handlers
{
    public class TaskHandlers :

        IRequestHandler<CreateNewTask, Unit>,
        IRequestHandler<CompleteTask, Unit>,
        IRequestHandler<IncreasePriority, Unit>,
        IRequestHandler<DecreasePriority, Unit>,
        IRequestHandler<SetTaskDueDate, Unit>,
        IRequestHandler<DeleteTask, Unit>
    {
        private readonly ISystemClock _systemClock;
        private readonly ITaskRepository _taskRepository;

        public TaskHandlers(ITaskRepository taskRepository, ISystemClock systemClock)
        {
            if (taskRepository == null)
                throw new ArgumentNullException("taskRepository");

            if (systemClock == null)
                throw new ArgumentNullException("systemClock");

            _taskRepository = taskRepository;
            _systemClock = systemClock;
        }

        public Unit Handle(CreateNewTask command)
        {
            var task = new Task(command.TaskId, command.Title, _systemClock);
            _taskRepository.Add(task);

            return Unit.Value;
        }

        public Unit Handle(CompleteTask command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.Complete();

            return Unit.Value;
        }

        public Unit Handle(IncreasePriority command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.IncreasePriority();

            return Unit.Value;
        }

        public Unit Handle(DecreasePriority command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.DecreasePriority();

            return Unit.Value;
        }

        public Unit Handle(SetTaskDueDate command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            task.SetDueDate(command.DueDate);

            return Unit.Value;
        }

        public Unit Handle(DeleteTask command)
        {
            var task = _taskRepository.GetById(command.TaskId);
            _taskRepository.Delete(task);

            return Unit.Value;
        }
    }
}