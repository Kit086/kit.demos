using AAADemo.Domain.Common;
using AAADemo.Domain.Entities;

namespace AAADemo.Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }
}