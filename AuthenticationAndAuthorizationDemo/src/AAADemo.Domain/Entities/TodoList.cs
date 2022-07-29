using AAADemo.Domain.Common;
using AAADemo.Domain.ValueObjects;

namespace AAADemo.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string? Title { get; set; }
    
    public Color Color { get; set; } = Color.White;
    
    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}