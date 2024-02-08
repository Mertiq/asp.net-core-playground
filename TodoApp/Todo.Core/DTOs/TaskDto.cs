namespace Todo.Core.DTOs;

public class TaskDto
{
    public string Content { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}