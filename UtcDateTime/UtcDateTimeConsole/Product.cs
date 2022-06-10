using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UtcDateTimeConsole;

public class Product
{
    public int ProductId { get; set; }
    [MaxLength(256)] public string Name { get; set; } = null!;
    [Precision(10, 2)] public decimal Price { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime? ModifiedUtc { get; set; }

    public override string ToString()
    {
        return
            $"ProductId: {ProductId}, Name: {Name}, Price: {Price}, CreatedUtc: {CreatedUtc}, ModifiedUtc: {ModifiedUtc}";
    }
}