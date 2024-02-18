namespace TodoApp.Domain.Users;
public interface IUserContext
{
    User CurrentUser { get; }
}
