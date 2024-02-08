using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Todo.Core.Entities.Task;

namespace Todo.Repository;

public class TaskSeed : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasData(
            new Task { Id = 1, Content = "ilk", IsDone = false }
        );
    }
}