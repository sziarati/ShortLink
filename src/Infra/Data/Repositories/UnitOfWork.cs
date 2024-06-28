using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.ShortLinks;
using Domain.Interfaces.Repository.Users;

namespace Infra.Data.Repositories;

public class UnitOfWork(AppDbContext dbContext, IShortLinkRepository shortLinkRepository, IUserRepository userRepository) : IUnitOfWork
{
    private readonly AppDbContext _dbContext = dbContext;
    public IShortLinkRepository _shortLinkRepository => shortLinkRepository;
    public IUserRepository _userRepository => userRepository;

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
