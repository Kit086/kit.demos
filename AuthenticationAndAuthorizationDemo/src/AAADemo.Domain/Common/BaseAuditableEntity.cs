namespace AAADemo.Domain.Common;

public class BaseAuditableEntity : BaseEvent
{
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}