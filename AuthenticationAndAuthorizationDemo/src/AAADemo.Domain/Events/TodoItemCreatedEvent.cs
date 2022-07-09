using AAADemo.Domain.Common;
using AAADemo.Domain.Entities;

namespace AAADemo.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }
}