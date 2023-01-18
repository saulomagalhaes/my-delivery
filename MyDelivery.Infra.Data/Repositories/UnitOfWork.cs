using Microsoft.EntityFrameworkCore.Storage;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Infra.Data.Context;

namespace MyDelivery.Infra.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDeliveryDbContext _db;
    private IDbContextTransaction _transaction;

    public UnitOfWork(MyDeliveryDbContext db)
    {
        _db = db;
    }

    public async Task BeginTransaction()
    {
        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _transaction.CommitAsync();
    }
    public async Task RollBack()
    {
        await _transaction.RollbackAsync();
    }
    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
