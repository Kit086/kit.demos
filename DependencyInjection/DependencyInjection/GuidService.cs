namespace DependencyInjection;

public class GuidServiceScoped
{
    public Guid Id { get; } = Guid.NewGuid();
}

public class GuidServiceSingleton
{
    public Guid Id { get; } = Guid.NewGuid();
}