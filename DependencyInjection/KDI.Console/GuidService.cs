namespace KDI.Console;

public interface IGuidServiceSingleton
{
    Guid Guid { get; }
}

public class GuidServiceSingleton : IGuidServiceSingleton
{
    public Guid Guid { get; } = Guid.NewGuid();
}

public interface IGuidServiceTransient
{
    Guid Guid { get; }
}

public class GuidServiceTransient : IGuidServiceTransient
{
    public Guid Guid { get; } = Guid.NewGuid();
}