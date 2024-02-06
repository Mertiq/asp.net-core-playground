namespace Todo.Core.UnitOfWorks;

public interface IUnitOfWorks
{
    Task CommitAsync();
    void Commit();
}