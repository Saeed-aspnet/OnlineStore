using CleanArchitectureSample.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Persistance.Context;

namespace OnlineStore.Persistance.Repositories;
public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(OnlineStoreDbContext dbContext) : base(dbContext) { }

    public bool IsProductExist(string productName)
    {
        return _dbContext.Products.AsNoTracking().Any(x => x.Title.ToLower() == productName.ToLower());
    }

    public async Task<Product?> GetById(int id)
    {
        return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product> Add(Product product)
    {
        await _dbContext.Products.AddAsync(product);

        return product;
    }
}