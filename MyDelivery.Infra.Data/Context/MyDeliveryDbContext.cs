using Microsoft.EntityFrameworkCore;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Infra.Data.Context;

public class MyDeliveryDbContext : DbContext
{
	public MyDeliveryDbContext(DbContextOptions<MyDeliveryDbContext> options): base(options){}

    public DbSet<Person> People { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Purchase> Purchases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDeliveryDbContext).Assembly);
    }
}
