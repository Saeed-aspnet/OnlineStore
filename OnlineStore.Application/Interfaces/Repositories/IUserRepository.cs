using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories;
public interface IUserRepository 
{
    Task<User?> GetById(int id);
}