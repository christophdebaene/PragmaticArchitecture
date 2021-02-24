using System;
using System.Collections.Generic;
using System.Linq;
using MyApp.Domain.Users;

namespace MyApp.Application.Bootstrapper
{
    public class MockUserRepository : IUserRepository
    {
        static readonly List<User> s_users = new List<User> {
            new User
            {
                Name = "Bart",
                Roles = Role.None
            },
            new User
            {
                Name = "Homer",
                Roles = Role.Contributor
            },
            new User
            {
                Name = "Lisa",
                Roles = Role.Administrator
            },
        };
        public IEnumerable<User> GetUsers()
        {
            return s_users.AsEnumerable();
        }
        public User GetUserByName(string username)
        {
            return string.IsNullOrEmpty(username)
                ? User.Unknown
                : s_users.SingleOrDefault(x => x.Name.Equals(username, StringComparison.OrdinalIgnoreCase)) ?? User.Unknown;
        }
    }
}
