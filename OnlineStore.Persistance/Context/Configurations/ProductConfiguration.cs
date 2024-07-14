using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Persistance.Context.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Title).IsUnique();
        builder.Property(p => p.Title).IsRequired().HasMaxLength(40);
        builder.Property(p => p.Price).HasColumnType("decimal(18,6)").IsRequired();
        builder.Property(p => p.InventoryCount).IsRequired();
        builder.Property(p => p.Discount).HasColumnType("decimal(4,2)").IsRequired();
    }
}