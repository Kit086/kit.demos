using Microsoft.EntityFrameworkCore;

namespace AutoMapperDemoConsole;

public static class DataSeeding
{
    public static void DoDataSeeding(ModelBuilder modelBuilder)
    {
        Address a1 = new Address
        {
            AddressId = 1,
            Country = "han",
            Province = "zhuojun",
            City = "zhuoxian",
            DetailAddress = "liubeijia",
            PersonId = 1
        };
        Address a3 = new Address
        {
            AddressId = 2,
            Country = "han",
            Province = "zhuojun",
            City = "unknown",
            DetailAddress = "zhangfeijia",
            PersonId = 3
        };

        Account ac1_1 = new Account
        {
            AccountId = 1,
            Username = "liuxuande",
            Password = "123456",
            EmailAddress = "liuxuande@han.com",
            PersonId = 1
        };

        Account ac1_2 = new Account
        {
            AccountId = 2,
            Username = "liuyuzhou",
            Password = "123456",
            PersonId = 1
        };

        Account ac2_1 = new Account
        {
            AccountId = 3,
            Username = "guanyunchang",
            Password = "123456",
            EmailAddress = "guanyunchang@han.com",
            PersonId = 2
        };

        Account ac2_2 = new Account
        {
            AccountId = 4,
            Username = "meirangong",
            Password = "123456",
            PersonId = 2
        };
        
        Person p1 = new()
        {
            PersonId = 1,
            FirstName = "Bei",
            LastName = "Liu",
            Age = 63,
            CreatedUtc = DateTime.UtcNow,
            ModifiedUtc = DateTime.UtcNow.AddDays(10)
        };
        
        Person p2 = new()
        {
            PersonId = 2,
            FirstName = "Yu",
            LastName = "Guan",
            Age = 60,
            CreatedUtc = DateTime.UtcNow,
            ModifiedUtc = DateTime.UtcNow.AddDays(10)
        };

        Person p3 = new()
        {
            PersonId = 3,
            FirstName = "Fei",
            LastName = "Zhang",
            Age = 58,
            CreatedUtc = DateTime.UtcNow
        };

        modelBuilder.Entity<Address>()
            .HasData(a1, a3);
        modelBuilder.Entity<Account>()
            .HasData(ac1_1, ac1_2, ac2_1, ac2_2);
        modelBuilder.Entity<Person>()
            .HasData(p1, p2, p3);
    }
}