using Ardalis.Result;
using Bricks;
using FluentValidation;
using Mediator;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Users;

[Command]
public record AddUser : IRequest<Result>
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
public class AddUserHandler(IApplicationDbContext context) : IRequestHandler<AddUser, Result>
{
    public async ValueTask<Result> Handle(AddUser request, CancellationToken cancellationToken)
    {
        var user = new User(request.UserId.ToString("D"), request.FirstName!, request.LastName!);
        await context.Users.AddAsync(user, cancellationToken);

        return Result.Success();
    }
}
