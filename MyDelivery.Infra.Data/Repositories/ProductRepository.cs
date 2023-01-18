using Microsoft.EntityFrameworkCore;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;
using MyDelivery.Infra.Data.Context;

namespace MyDelivery.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MyDeliveryDbContext _db;

    public ProductRepository(MyDeliveryDbContext db)
    {
        _db = db;
    }

    public async Task<Product> Create(Product product)
    {
        _db.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task Delete(Product product)
    {
        _db.Remove(product);
        await _db.SaveChangesAsync();
       
    }

    public async Task<Product> GetById(int id)
    {
        return await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> GetIdByCode(string code)
    {
        return (await _db.Products.FirstOrDefaultAsync(p => p.Code == code))?.Id ?? 0; 
    }

    public async Task<ICollection<Product>> GetProducts(int page, int rows)
    {
        return await _db.Products.Skip((page - 1) * rows).Take(rows).ToListAsync();
    }

    public async Task Update(Product product)
    {
        _db.Update(product);
        await _db.SaveChangesAsync();
    }
}
