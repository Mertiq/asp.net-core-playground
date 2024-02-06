namespace Todo.Core.Entities;

public class Task
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}