using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Tasks;

namespace TodoApp.Infrastructure.Database.Configurations;
public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable(nameof(TaskItem));
        builder.Property(x => x.Priority).HasConversion<string>();
    }
}
