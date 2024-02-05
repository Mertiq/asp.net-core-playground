namespace Todo.Todo.Core.Models;

public class Item
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}