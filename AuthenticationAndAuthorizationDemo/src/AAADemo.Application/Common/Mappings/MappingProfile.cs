using System.Reflection;
using AutoMapper;

namespace AAADemo.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(HasInterface))
            .ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            
            if (type.GetMethod(mappingMethodName) is not { } methodInfo)
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Any())
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
            else
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
        }
    }
}