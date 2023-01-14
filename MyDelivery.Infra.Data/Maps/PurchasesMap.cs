using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDelivery.Domain.Entities;
namespace MyDelivery.Infra.Data.Maps;

public class PurchasesMap : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.ToTable("purchases");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.PersonId)
            .HasColumnName("person_id");

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id");

        builder.Property(x => x.Date)
            .HasColumnType("date")
            .HasColumnName("date");

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Purchases);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Purchases);
    }
}
