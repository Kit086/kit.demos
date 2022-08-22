namespace KDI;

public class KServiceCollection : List<KServiceDescriptor>
{
    public KServiceProvider BuildKServiceProvider()
    {
        return new KServiceProvider(this);
    }
    
    public KServiceCollection AddSingleton<TService, TImplement>()
    {
        var descriptor = AddDescriptor<TService, TImplement>(LifeTime.Singleton);
        Add(descriptor);
        return this;
    }

    public KServiceCollection AddTransient<TService, TImplement>()
    {
        var descriptor = AddDescriptor<TService, TImplement>(LifeTime.Transient);
        Add(descriptor);
        return this;
    }
    
    private static KServiceDescriptor AddDescriptor<TService, TImplement>(LifeTime lifeTime)
    {
        var descriptor = new KServiceDescriptor
        {
            ServiceType = typeof(TService),
            ImplementType = typeof(TImplement),
            LifeTime = lifeTime
        };
        return descriptor;
    }
}