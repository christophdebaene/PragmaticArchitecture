namespace MyApp.Domain.Shared
{
    public interface IAuditable
    {
        AuditInfo Audit { get; }
    }
}
