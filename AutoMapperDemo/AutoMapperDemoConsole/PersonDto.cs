using AutoMapper;

namespace AutoMapperDemoConsole;

public class PersonDto
{
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string? Address { get; set; } = null!;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }

    public void Display() =>
        Console.WriteLine(
            $"Name: {Name}, Age: {Age}, Address: {Address}, Accounts Number: {Accounts.Count}, Created: {Created}, Modified: {Modified}");
}

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        // string? name = null;
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            // .ForMember(dest => dest.Name, opt => opt.MapFrom(src => name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address != null ? $"{src.Address.Country}, {src.Address.Province}, {src.Address.City}, {src.Address.DetailAddress}" : null))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.CreatedUtc.ToLocalTime()))
            .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.ModifiedUtc.HasValue ? src.ModifiedUtc.Value.ToLocalTime() : (DateTime?)null));
    }
}