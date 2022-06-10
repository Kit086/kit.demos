using Mapster;

namespace UtcDateTimeConsole;

public class MyMapsterRegister : ICodeGenerationRegister
{
    public void Register(CodeGenerationConfig config)
    {
        config.AdaptTo("[name]Dto")
            .ForType<Product>(cfg =>
            {
                cfg.Map(poco => poco.CreatedUtc, "Created");
                cfg.Map(poco => poco.ModifiedUtc, "Modified");
            });
        
        config.GenerateMapper("[name]Mapper")
            .ForType<Product>();
    }
}