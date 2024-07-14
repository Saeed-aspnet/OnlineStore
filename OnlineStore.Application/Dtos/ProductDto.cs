namespace OnlineStore.Application.Dtos;
public class ProductDto
{
    public ProductDto()
    {
        Title = null!;
    }
    public string Title { get; set; }
    public int InventoryCount { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}