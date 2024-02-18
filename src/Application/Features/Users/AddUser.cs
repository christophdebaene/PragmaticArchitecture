using Bricks;
using FluentValidation;
using MediatR;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Users;

[Command]
public record AddUser : IRequest
{
    public Guid UserId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
public class AddUserValidator : AbstractValidator<AddUser>
{
    public AddUserValidator()
    {
        RuleFor(x => x.FirstName).Length(1, 255);
        RuleFor(x => x.LastName).Length(1, 255);
    }
}
public class AddUserHandler(IApplicationDbContext context) : IRequestHandler<AddUser>
{
    public async Task Handle(AddUser request, CancellationToken cancellationToken)
    {
        var user = new User(request.UserId.ToString("D"), request.FirstName!, request.LastName!);
        await context.Users.AddAsync(user, cancellationToken);
    }
}
