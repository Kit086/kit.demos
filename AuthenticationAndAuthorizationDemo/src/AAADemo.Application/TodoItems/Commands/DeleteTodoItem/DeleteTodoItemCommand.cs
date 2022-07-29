using System.Globalization;
using AAADemo.Application.Common.Exceptions;
using AAADemo.Application.Common.Interfaces;
using AAADemo.Domain.Entities;
using AAADemo.Domain.Events;
using MediatR;

namespace AAADemo.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (await _context.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken)
            is not { } entity)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        _context.TodoItems.Remove(entity);
        
        entity.AddDomainEvent(new TodoItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}