using AAADemo.Application.Common.Mappings;
using AutoMapper;

namespace AAADemo.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;
    
    public MappingTests()
    {
        _configuration = new MapperConfiguration(
            config => config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Theory]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = 
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) is not null)
        {
            return Activator.CreateInstance(type)!;
        }
        
        return 
    }
}