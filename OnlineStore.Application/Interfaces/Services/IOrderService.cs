using OnlineStore.Application.Wrappers;

namespace OnlineStore.Application.Interfaces.Services;
public interface IOrderService
{
    Task<BaseResult<bool>> BuyProduct(int  productId, int userId);
}