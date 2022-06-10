namespace UtcDateTimeConsole;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    
    public override string ToString()
    {
        return
            $"ProductId: {ProductId}, Name: {Name}, Price: {Price}, Created: {Created}, Modified: {Modified}";
    }
}