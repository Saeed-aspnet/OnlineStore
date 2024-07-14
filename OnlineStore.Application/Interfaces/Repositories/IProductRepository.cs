using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;

namespace CleanArchitectureSample.Application.Interfaces.Repositories;
public interface IProductRepository
{
    bool IsProductExist(string productName);
    Task<Product?> GetById(int id);
    Task<Product> Add(Product product);
}