namespace MyDelivery.Domain.Contracts.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransaction();
    Task Commit();
    Task RollBack();
}
