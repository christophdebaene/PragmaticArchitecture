using FluentValidation;
using MediatR;
using System;

namespace MyApp.Application.Commands
{
    public record CreateNewTask : IRequest<Unit>
    {
        public Guid TodoId { get; init; }
        public string Title { get; init; }
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