using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;
public sealed class Order : BaseEntity
{
    public Order() { }


    public Order(int productId,int buyerId)
    {
        ProductId = productId;
        CreationDate = DateTime.Now;
        BuyerId = buyerId;
    }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public DateTime CreationDate { get; private set; }
    public int BuyerId { get; private set; }
    public User Buyer { get; private set; }
}