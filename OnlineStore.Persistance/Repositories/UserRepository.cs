using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Persistance.Context;

namespace OnlineStore.Persistance.Repositories;
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(OnlineStoreDbContext dbContext) : base(dbContext) { }

    public async Task<User?> GetById(int id)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}