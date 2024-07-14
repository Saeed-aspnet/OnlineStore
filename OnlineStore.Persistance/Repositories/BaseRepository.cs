using OnlineStore.Persistance.Context;

namespace OnlineStore.Persistance.Repositories;
public abstract class BaseRepository
{
    protected readonly OnlineStoreDbContext _dbContext;
    protected BaseRepository(OnlineStoreDbContext dbContext) { _dbContext = dbContext; }
}