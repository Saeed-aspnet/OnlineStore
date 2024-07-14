using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;
public interface IOrderRepository 
{
    Task<Order> Add(Order order);
}