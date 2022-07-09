using EFCoreWebAPP.Application.Entities.Common;

namespace EFCoreWebAPP.Application.Entities;

public class TodoList : BaseAuditableEntity
{
    public string Title { get; set; }
    public ICollection<TodoItem> Items { get; set; } = new List<TodoItem>();
}

public class TodoItem : BaseAuditableEntity
{
    
}