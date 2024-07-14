using OnlineStore.Application.Dtos;
using OnlineStore.Application.Wrappers;

namespace OnlineStore.Application.Interfaces.Services;
public interface IProductService
{
    Task<BaseResult<int>> AddProduct(ProductDto productDto);
    Task<BaseResult<bool>> UpdateProduct(int id, int inventoryCount);
    Task<BaseResult<ProductDto>> GetProduct(int id);
}