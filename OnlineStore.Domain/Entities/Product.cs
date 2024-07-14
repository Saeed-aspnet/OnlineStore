using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;
public sealed class Product : BaseEntity
{
    public Product()
    {
        Title = null!;
    }
    public Product(string title, int inventoryCount, decimal price, decimal discount)
    {
        Title = title;
        InventoryCount = inventoryCount;
        Price = price;
        Discount = discount;
    }

    public void IncreaseInventoryCount(int amount)
    {
        InventoryCount += amount;
    }

    public void DecreasseInventoryCount()
    {
        if (InventoryCount <= 0)
            throw new ArgumentException("Product out of stock");

        InventoryCount--;
    }

    public bool IsProductInventoryOutOfStock()
    {
        return InventoryCount <= 0;
    }

    public string Title { get; private set; }
    public int InventoryCount { get; private set; }
    public decimal Price { get; private set; }
    public decimal Discount { get; private set; }
}