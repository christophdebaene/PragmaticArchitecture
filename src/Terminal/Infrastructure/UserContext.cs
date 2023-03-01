using MyApp.Domain.Users;

namespace Terminal.Infrastructure;
public class UserContext : IUserContext
{
    public User CurrentUser { get; set; }
}
