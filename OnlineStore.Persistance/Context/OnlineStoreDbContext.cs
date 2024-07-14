using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistance.Context;
public class OnlineStoreDbContext : DbContext
{
    public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<User>()
                    .HasData(
                             new { Id = 1, Name = "UserA" },
                             new { Id = 2, Name = "UserB" }
                            );

        modelBuilder.Entity<Product>()
                    .HasData
                    (
                       new { Id = 1, Title = "ProductA", InventoryCount = 10, Price = 1000m, Discount = 2m },
                       new { Id = 2, Title = "ProductB", InventoryCount = 5, Price = 3000m, Discount = 1m }
                    );

        base.OnModelCreating(modelBuilder);
    }
}