using AAADemo.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AAADemo.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler : INotificationHandler<TodoItemCompletedEvent>
{
    private readonly ILogger<TodoItemCompletedEventHandler> _logger;

    public TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AAADemo Domain Event: {DomainName}", notification.GetType().Name);
        
        return Task.CompletedTask;
    }
}