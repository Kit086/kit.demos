using AAADemo.Application.Common.Exceptions;
using AAADemo.Application.Common.Interfaces;
using AAADemo.Application.TodoItems.Commands.UpdateTodoItem;
using AAADemo.Domain.Entities;
using AAADemo.Domain.Enums;
using MediatR;

namespace AAADemo.Application.TodoItems.Commands.UpdateTodoItemDetail;

public class UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        if (await _context.TodoItems.FindAsync(new object[] { request.Id }, cancellationToken)
            is not { } entity)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}