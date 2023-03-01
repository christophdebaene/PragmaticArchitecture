namespace MyApp.Domain.Users;
public interface IUserContext
{
    User CurrentUser { get; }
}
