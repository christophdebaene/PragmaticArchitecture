namespace TodoApp.Domain.Users;
public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetUsers();
    Task<User> GetUserById(string id);
}
