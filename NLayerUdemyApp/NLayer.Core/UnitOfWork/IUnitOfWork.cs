namespace NLayer.Core.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    void Commit();
}