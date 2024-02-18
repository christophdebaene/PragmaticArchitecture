namespace TodoApp.Domain.Users;
public class User
{
    public string Id { get; set; }    
    public string FirstName { get; set; }
    public string LastName { get; set; }    
    public string SubscriptionLevel { get; set; }    
    public string Country { get; set; }
    public Role Roles { get; set; }

    public bool IsInRole(Role role)
    {
        return Roles.HasFlag(role);
    }
    public User()
    {
    }
    public User(string id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Roles = Role.None;
    }

    public static readonly User Unknown = new UnknownUser();
}

public class UnknownUser : User
{
    public UnknownUser()
    {
        Id = "Unknown";
        FirstName = "Unknown";
        LastName = "Unknown";
        SubscriptionLevel = null;
        Country = null;
    }
}
