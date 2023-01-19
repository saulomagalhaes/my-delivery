using MyDelivery.Domain.Entities;

namespace MyDelivery.Domain.Contracts.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAndPassword(string email, string password);
}
