using OnlineStore.Domain.Common;

namespace OnlineStore.Domain.Entities;
public sealed class User : BaseEntity
{
    public User()
    {
        Name = null!;
    }
    public User(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public ICollection<Order> Orders { get; private set; }
}