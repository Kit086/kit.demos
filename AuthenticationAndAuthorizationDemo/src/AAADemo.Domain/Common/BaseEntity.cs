using System.ComponentModel.DataAnnotations.Schema;

namespace AAADemo.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    
    private readonly List<BaseEvent> _domainsEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainsEvents => _domainsEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainsEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainsEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainsEvents.Clear();
    }
}