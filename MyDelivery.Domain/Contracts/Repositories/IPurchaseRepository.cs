using MyDelivery.Domain.Entities;

namespace MyDelivery.Domain.Contracts.Repositories;

public interface IPurchaseRepository
{
    Task<ICollection<Purchase>> GetPurchases();
    Task<Purchase> GetById(int id);
    Task<Purchase> Create(Purchase purchase);
    Task Update(Purchase purchase);
    Task Delete(Purchase purchase);
    Task<ICollection<Purchase>> GetByPersonId(int personId);
    Task<ICollection<Purchase>> GetByProductId(int productId);
}
