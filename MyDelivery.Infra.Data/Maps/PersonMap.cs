using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDelivery.Domain.Entities;
namespace MyDelivery.Infra.Data.Maps;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("people");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(x => x.Document)
            .HasColumnName("document");

        builder.Property(x => x.Phone)
            .HasColumnName("phone");

        builder.HasMany(x => x.Purchases)
            .WithOne(x => x.Person)
            .HasForeignKey(x => x.PersonId);
    }
}
