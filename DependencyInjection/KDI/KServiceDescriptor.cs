namespace KDI;

public class KServiceDescriptor
{
    public Type ServiceType { get; set; } = default!;
    
    public Type? ImplementType { get; set; }

    public LifeTime LifeTime { get; set; }
}

public enum LifeTime
{
    Transient,
    Singleton
}