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
public class GetUsersHandler : IRequestHandler<GetUsers, List<UserView>>
{
    private readonly MyAppContext _context;
    public GetUsersHandler(MyAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<List<UserView>> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var result = _context.Users.Select(x => new UserView
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
