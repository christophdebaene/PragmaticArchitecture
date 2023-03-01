namespace MyApp.Domain.Users;

[Flags]
public enum Role
{
    None = 0,
    Administrator = 1,
    Contributor = 2
}
