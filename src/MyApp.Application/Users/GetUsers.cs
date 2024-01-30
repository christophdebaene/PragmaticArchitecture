using Bricks;
using MediatR;
using MyApp.Domain;
using MyApp.Domain.Users;

namespace MyApp.Application.Users;

[Query]
public record GetUsers : IRequest<List<UserView>>
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
public class GetUsersHandler(MyAppContext context) : IRequestHandler<GetUsers, List<UserView>>
{    
    public async Task<List<UserView>> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var result = context.Users.Select(x => new UserView
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            SubscriptionLevel = x.SubscriptionLevel,
            Roles = x.Roles
        });

        return result.ToList();
    }
}
