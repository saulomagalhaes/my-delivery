using Microsoft.EntityFrameworkCore;
using MyDelivery.Domain.Entities;

namespace MyDelivery.Infra.Data.Context;

public class MyDeliveryDbContext : DbContext
{
	public MyDeliveryDbContext(DbContextOptions<MyDeliveryDbContext> options): base(options){}

    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDeliveryDbContext).Assembly);
    }
}
