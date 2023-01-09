using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDelivery.Domain.Entities;
namespace MyDelivery.Infra.Data.Maps;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(x => x.Code)
            .HasColumnName("code");

        builder.Property(x => x.Price)
            .HasColumnName("price");

        builder.HasMany(x => x.Purchases)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}
