namespace TodoApp.Infrastructure.Database.Models;
public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public DateTimeOffset? ProcessedOn { get; set; }
    public string? Error { get; set; }
}
