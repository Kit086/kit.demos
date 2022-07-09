using AAADemo.Domain.Enums;

namespace AAADemo.Domain.Entities;

public class TodoItem
{
    public string Title { get; set; } = null!;
    public string? Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime Reminder { get; set; }
    public bool Done { get; set; }

    #region relationships

    public int TodoListId { get; set; }
    public TodoList TodoList { get; set; } = null!;

    #endregion
}

public class TodoList
{
    public int TodoListId { get; set; }
    public string Title { get; set; } = null!;
}