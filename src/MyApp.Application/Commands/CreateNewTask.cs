using FluentValidation;
using SlickBus;
using System;

namespace MyApp.Application.Commands
{
    public class CreateNewTask : Request<Unit>
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
    }

    public class CreateNewTaskValidator : AbstractValidator<CreateNewTask>
    {
        public CreateNewTaskValidator()
        {
            RuleFor(x => x.TaskId).NotEmpty();
            RuleFor(x => x.Title).Length(1, 255);
        }
    }
}