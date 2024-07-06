using Domain.Entities.ShortLinkAggregate;
using Domain.Entities.UserAggregate;
using Domain.Interfaces.Repository;

namespace Infra.Data.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
