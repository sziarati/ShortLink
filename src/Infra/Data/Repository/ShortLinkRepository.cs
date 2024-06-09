using Domain.Entities.ShortLinkAggregate;
using Domain.Entities.UserAggregate;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class ShortLinkRepository(AppDbContext appDbContext) : IShortLinkRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public Task<decimal> AddAsync(ShortLink shortLink)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(decimal id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ShortLink>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<ShortLink>> GetAllExpiredShortLinksAsync()
    {
        var shortLinks = await _appDbContext.ShortLinks
                                             .Where(i => DateTime.Compare(i.ExpireDate, DateTime.Now) <= 0)
                                             .ToListAsync();
        return shortLinks;
    }

    public Task<ShortLink> GetByIdAsync(decimal id)
    {
        throw new NotImplementedException();
    }

    public Task<ShortLink?> GetShortLinkAsync(string uniqueCode)
    {
        throw new NotImplementedException();
    }
}
