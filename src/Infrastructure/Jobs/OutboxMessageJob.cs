using System.Text.Json;
using Bricks.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using TodoApp.Infrastructure.Database;

namespace TodoApp.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class OutboxMessageJob(ApplicationDbContext dbContext, IPublisher publisher, TimeProvider timeProvider) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await dbContext.Outbox
            .Where(x => x.ProcessedOn == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach(var outboxMessage in messages)
        {
            var domainEvent = JsonSerializer.Deserialize<IDomainEvent>(outboxMessage.Content);
            if (domainEvent is null)
            {
                continue;
            }

            await publisher.Publish(domainEvent, context.CancellationToken);
            outboxMessage.ProcessedOn = timeProvider.GetUtcNow();
        }

        await dbContext.SaveChangesAsync(context.CancellationToken);
    }
}
