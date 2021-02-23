namespace Bricks.Domain
{
    public interface IAuditable
    {
        AuditInfo Audit { get; }
    }
}
