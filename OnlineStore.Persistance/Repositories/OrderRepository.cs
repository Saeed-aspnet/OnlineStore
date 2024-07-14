using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Persistance.Context;

namespace OnlineStore.Persistance.Repositories;
public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(OnlineStoreDbContext dbContext) : base(dbContext) { }

    public async Task<Order> Add(Order order)
    {
        await _dbContext.Orders.AddAsync(order);

        return order;
    }
}