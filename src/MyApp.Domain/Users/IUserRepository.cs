using System.Collections.Generic;

namespace MyApp.Domain.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByName(string username);
    }
}
