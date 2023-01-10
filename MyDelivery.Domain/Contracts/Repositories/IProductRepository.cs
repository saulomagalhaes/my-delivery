using MyDelivery.Domain.Entities;

namespace MyDelivery.Domain.Contracts.Repositories;

public interface IProductRepository
{
    Task<ICollection<Product>> GetProducts();
    Task<Product> GetById(int id);
    Task<Product> Create(Product product);
    Task Update(Product product);
    Task Delete(Product product);
}
