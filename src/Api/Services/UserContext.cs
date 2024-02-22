using TodoApp.Domain.Users;

namespace TodoApp.Api.Services;
public class UserContext : IUserContext
{
    public User CurrentUser => User.Unknown;
}
