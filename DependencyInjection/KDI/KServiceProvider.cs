namespace KDI;

public class KServiceProvider
{
    private readonly Dictionary<Type, Func<object>> _transientDict = new();
    private readonly Dictionary<Type, Lazy<object>> _singletonDict = new();

    internal KServiceProvider(KServiceCollection collection)
    {
        foreach (var descriptor in collection)
        {
            if (descriptor.LifeTime is LifeTime.Transient)
            {
                _transientDict.Add(descriptor.ServiceType,
                    () => Activator.CreateInstance(
                        descriptor.ImplementType,
                        GetDependencies(descriptor)));
            }
            else if (descriptor.LifeTime is LifeTime.Singleton)
            {
                _singletonDict.Add(descriptor.ServiceType,
                    new Lazy<object>(() =>
                        Activator.CreateInstance(
                            descriptor.ImplementType, 
                            GetDependencies(descriptor))));
            }
        }
    }

    public T? GetKService<T>()
    {
        return (T?)GetKService(typeof(T));
    }

    private object? GetKService(Type type)
    {
        if (_singletonDict.TryGetValue(type, out var lazyObj))
        {
            return lazyObj.Value;
        }

        if(_transientDict.TryGetValue(type, out var func))
        {
            return func.Invoke();
        }

        throw new Exception("You have not registered the service.");
    }

    private object?[] GetDependencies(KServiceDescriptor descriptor)
    {
        return descriptor.ImplementType
            .GetConstructors().First()
            .GetParameters()
            .Select(pi => GetKService(pi.ParameterType))
            .ToArray();
    }
}