namespace MyApp.Domain.Users
{
    public class User
    {
        public string Name { get; set; }
        public Role Roles { get; set; }
        public bool IsInRole(Role role)
            => Roles.HasFlag(role);

        public static readonly User Unknown = new UnknownUser();
    }
    public class UnknownUser : User
    {
        public UnknownUser()
        {
            Name = "Unknown";
            Roles = Role.None;
        }
    }
}
