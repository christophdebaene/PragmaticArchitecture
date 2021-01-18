using FluentValidation;
using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public class CreateNewTask : IRequest<Unit>
    {
        public Guid TodoId { get; set; }
        public string Title { get; set; }
    }

    public class CreateNewTaskValidator : AbstractValidator<CreateNewTask>
    {
        public CreateNewTaskValidator()
        {
            RuleFor(x => x.TodoId).NotEmpty();
            RuleFor(x => x.Title).Length(1, 255);
        }
    }
}