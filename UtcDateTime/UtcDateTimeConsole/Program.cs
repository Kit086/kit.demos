// See https://aka.ms/new-console-template for more information

using Mapster;
using UtcDateTimeConsole;

using ApplicationDbContext dbContext = new();

var product = new Product
{
    Name = "Coca-Cola",
    Price = 3.5M,
    CreatedUtc = DateTime.UtcNow,
    ModifiedUtc = DateTime.UtcNow.AddMinutes(10)
};

dbContext.Products.Add(product);
dbContext.SaveChanges();

using ApplicationDbContext queryDbContext = new();

var myProduct = queryDbContext.Products.SingleOrDefault(p => p.Name == "Coca-Cola");
if (myProduct is null)
{
    throw new NullReferenceException("Cannot find the product named \"Coca-Cola\"");
}

Console.WriteLine(myProduct);
TypeAdapterConfig typeAdapterConfig = getProductTypeAdapterConfig();
ProductDto productDto = myProduct.Adapt<ProductDto>(typeAdapterConfig);
Console.WriteLine(productDto);
Console.WriteLine(productDto.Created.Kind);
Console.WriteLine(productDto.Modified?.Kind);

// DateTime created = myProduct.CreatedUtc.ToLocalTime();
// DateTime? modified = myProduct.ModifiedUtc?.ToLocalTime();
// Console.WriteLine(created);
// Console.WriteLine(modified);

static TypeAdapterConfig getProductTypeAdapterConfig()
{
    TypeAdapterConfig config = new();
    config.NewConfig<Product, ProductDto>()
        .Map(dest => dest.Created, src => src.CreatedUtc.ToLocalTime())
        .Map(dest => dest.Modified, src => src.ModifiedUtc != null 
            ? src.ModifiedUtc.Value.ToLocalTime() 
            : (DateTime?)null);
    return config;
}

// Console.WriteLine(myProduct.CreatedUtc.Kind);
// Console.WriteLine(myProduct.ModifiedUtc?.Kind);

#region MyRegion

// myProduct.CreatedUtc = myProduct.CreatedUtc.ToLocalTime(); // 将 CreatedUtc 转为本地时间
// Console.WriteLine(myProduct);

// var utcNow = DateTime.UtcNow;
// var now = DateTime.Now;
// var localDateTime = utcNow.ToLocalTime(); // UTC 时间转本地时间
// var localLocalDateTime = localDateTime.ToLocalTime();
// var unspecifiedDateTime = DateTime.SpecifyKind(now, DateTimeKind.Unspecified);
// var unspecifiedToLocalDateTime = unspecifiedDateTime.ToLocalTime();
//
// Console.WriteLine($"utcNow: {utcNow}");
// Console.WriteLine($"utcNow.Kind: {utcNow.Kind}");
// Console.WriteLine($"now: {now}");
// Console.WriteLine($"now.Kind: {now.Kind}");
// Console.WriteLine($"localDateTime: {localDateTime}");
// Console.WriteLine($"localDateTime.Kind: {localDateTime.Kind}");
// Console.WriteLine($"localLocalDateTime: {localLocalDateTime}");
// Console.WriteLine($"localLocalDateTime.Kind: {localLocalDateTime.Kind}");
// Console.WriteLine($"unspecifiedDateTime: {unspecifiedDateTime}");
// Console.WriteLine($"unspecifiedDateTime.Kind: {unspecifiedDateTime.Kind}");
// Console.WriteLine($"unspecifiedToLocalDateTime: {unspecifiedToLocalDateTime}");
// Console.WriteLine($"unspecifiedToLocalDateTime.Kind: {unspecifiedToLocalDateTime.Kind}");
// Console.ReadLine();

#endregion