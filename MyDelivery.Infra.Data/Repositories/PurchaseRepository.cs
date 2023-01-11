using Microsoft.EntityFrameworkCore;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;
using MyDelivery.Infra.Data.Context;

namespace MyDelivery.Infra.Data.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly MyDeliveryDbContext _db;

    public PurchaseRepository(MyDeliveryDbContext db)
    {
        _db = db;
    }

    public async Task<Purchase> Create(Purchase purchase)
    {
        _db.Add(purchase);
        await _db.SaveChangesAsync();
        return purchase;
    }

    public async Task Delete(Purchase purchase)
    {
        _db.Remove(purchase);
        await _db.SaveChangesAsync();
    }

    public async Task<Purchase> GetById(int id)
    {
        var purchase = await _db.Purchases
                .Include(p => p.Product)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(p => p.Id == id);

        return purchase;
    }

    public async Task<ICollection<Purchase>> GetByPersonId(int personId)
    {
        return await _db.Purchases
                .Include(p => p.Product)
                .Include(p => p.Person)
                .Where(p => p.PersonId == personId).ToListAsync();
    }

    public async Task<ICollection<Purchase>> GetByProductId(int productId)
    {
        return await _db.Purchases
                .Include(p => p.Product)
                .Include(p => p.Person)
                .Where(p => p.ProductId == productId).ToListAsync();
    }

    public async Task<ICollection<Purchase>> GetPurchases()
    {
        return await _db.Purchases
                .Include(p => p.Product)
                .Include(p => p.Person)
                .ToListAsync();
    }

    public async Task Update(Purchase purchase)
    {
        _db.Update(purchase);
        await _db.SaveChangesAsync();
    }
}
