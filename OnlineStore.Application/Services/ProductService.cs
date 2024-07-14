using CleanArchitectureSample.Application.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Application.Dtos;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Application.Wrappers;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _uow;
    private readonly IMemoryCache _cache;

    public ProductService(IProductRepository productRepository, IUnitOfWork uow, IMemoryCache cache)
    {
        _productRepository = productRepository;
        _uow = uow;
        _cache = cache;
    }

    public async Task<BaseResult<int>> AddProduct(ProductDto productDto)
    {
        if (_productRepository.IsProductExist(productDto.Title))
            throw new InvalidOperationException("A product with the same name is exist");


        var model = new Product(productDto.Title, productDto.InventoryCount,
                                productDto.Price, productDto.Discount);

        var product = await _productRepository.Add(model);
        await _uow.SaveChangesAsync();

        return new BaseResult<int>
        {
            Success = true,
            Data = product.Id
        };
    }

    public async Task<BaseResult<bool>> UpdateProduct(int id, int inventoryCount)
    {
        var product = await _productRepository.GetById(id) ?? throw new ArgumentNullException($"Product with the id:{id} not found.");

        product.IncreaseInventoryCount(inventoryCount);

        await _uow.SaveChangesAsync();

        return new BaseResult<bool>
        {
            Success = true
        };
    }

    private async Task<Product?> CacheProduct(int id)
    {
        var cacheKey = $"Product_{id}";
        var cacheData = _cache.Get<Product>(cacheKey);

        if (cacheData is null)
        {
            var product = await _productRepository.GetById(id) ?? throw new ArgumentNullException($"Product with the id:{id} not found.");

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            _cache.Set(cacheKey, product, cacheOptions);

            return product;
        }

        return cacheData;
    }

    public async Task<BaseResult<ProductDto>> GetProduct(int id)
    {
        var product = await CacheProduct(id) ?? throw new ArgumentNullException($"Product with the id:{id} not found.");

        var priceWithDiscount = product.Price * (1 - product.Discount / 100);

        return new BaseResult<ProductDto>
        {
            Data = new ProductDto
            {
                InventoryCount = product.InventoryCount,
                Title = product.Title,
                Price = priceWithDiscount,
                Discount = product.Discount
            },
            Success = true
        };
    }
}