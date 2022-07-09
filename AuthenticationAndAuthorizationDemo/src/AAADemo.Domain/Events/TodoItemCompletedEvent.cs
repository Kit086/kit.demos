using AAADemo.Domain.Common;
using AAADemo.Domain.Entities;

namespace AAADemo.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }
}