using AAADemo.Domain.Common;
using AAADemo.Domain.Enums;
using AAADemo.Domain.Events;

namespace AAADemo.Domain.Entities;

public class TodoItem : BaseAuditableEntity
{
    public string? Title { get; set; }
    
    public string? Note { get; set; }
    
    public PriorityLevel Priority { get; set; }
    
    public DateTime? Reminder { get; set; }

    private bool _done;

    public bool Done
    {
        get => _done;
        set
        {
            if (value == true && _done == false)
            {
                AddDomainEvent(new TodoItemCompletedEvent(this));
            }

            _done = value;
        }
    }

    #region relationships

    public int ListId { get; set; }
    
    public TodoList List { get; set; } = null!;

    #endregion
}