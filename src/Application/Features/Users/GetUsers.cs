using Ardalis.Result;
using Mediator;
using TodoApp.Domain.Users;

namespace TodoApp.Application.Features.Users;
public record GetUsers : IQuery<Result<IReadOnlyList<UserView>>>
{
}
public record UserView
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string SubscriptionLevel { get; init; }
    public Role Roles { get; init; }
}
public class GetUsersHandler(IApplicationDbContext context) : IQueryHandler<GetUsers, Result<IReadOnlyList<UserView>>>
{
    public ValueTask<Result<IReadOnlyList<UserView>>> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var result = context.Users.Select(x => new UserView
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            SubscriptionLevel = x.SubscriptionLevel,
            Roles = x.Roles
        }).ToList();

        return ValueTask.FromResult(Result.Success<IReadOnlyList<UserView>>(result));
    }
}
