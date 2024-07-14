using CleanArchitectureSample.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Application.Wrappers;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;
public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;
    private IProductRepository _productRepository;
    private IUserRepository _userRepository;
    private IUnitOfWork _uow;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, IUnitOfWork uow)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task<BaseResult<bool>> BuyProduct(int productId, int userId)
    {
        var product = await _productRepository.GetById(productId) ??
                      throw new ArgumentNullException($"Product with id:{productId} not found.");

        if (product.InventoryCount <= 0)
            throw new ArgumentException("Product is out of stock.");

        var user = await _userRepository.GetById(userId);
        if (user is null)
             throw new ArgumentException("User not found.");

        product.DecreasseInventoryCount();

        var order = new Order(productId, userId);
        await _orderRepository.Add(order);

        await _uow.SaveChangesAsync();

        return new BaseResult<bool> { Success = true };
    }
}
