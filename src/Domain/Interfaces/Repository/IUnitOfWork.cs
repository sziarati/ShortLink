namespace Domain.Interfaces.Repository;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync();
}
