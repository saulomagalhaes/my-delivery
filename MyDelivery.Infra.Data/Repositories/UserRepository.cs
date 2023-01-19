using Microsoft.EntityFrameworkCore;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Domain.Entities;
using MyDelivery.Infra.Data.Context;

namespace MyDelivery.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MyDeliveryDbContext _db;

    public UserRepository(MyDeliveryDbContext db)
    {
        _db = db;
    }

    public async Task<User> GetUserByEmailAndPassword(string email, string password)
    {
        return await _db.Users
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}