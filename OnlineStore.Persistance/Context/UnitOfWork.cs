using OnlineStore.Application.Interfaces;

namespace OnlineStore.Persistance.Context;
public class UnitOfWork: IUnitOfWork
{
    private readonly OnlineStoreDbContext _dbContext;
    public UnitOfWork(OnlineStoreDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public bool SaveChanges()
    {
        return _dbContext.SaveChanges() > 0;
    }
}