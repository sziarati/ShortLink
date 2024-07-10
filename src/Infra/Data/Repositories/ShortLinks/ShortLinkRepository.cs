using Domain.Entities.ShortLinkAggregate;
using Domain.Interfaces.Repository.ShortLinks;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories.ShortLinks;

public class ShortLinkRepository(AppDbContext appDbContext) : IShortLinkRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(ShortLink shortLink)
    {
        await _appDbContext.ShortLinks.AddAsync(shortLink);
    }

    public async Task<bool> Delete(decimal id)
    {
        var shortLink = await GetByIdAsync(id);
        if (shortLink == null) 
        { 
            return false;
        }

        _appDbContext.ShortLinks.Remove(shortLink);
        return true;
    }

    public async Task<bool> SetExpired(int id)
    {
        var shortLink = await GetByIdAsync(id);
        shortLink.ExpireShortLink();
        _appDbContext.Entry(shortLink).State = EntityState.Modified;        
        return true;
    }

    public async Task<List<ShortLink>> GetAllAsync()
    {
        return await _appDbContext.ShortLinks.ToListAsync();
    }

    public async Task<IReadOnlyList<ShortLink>> GetAllExpiredShortLinksAsync()
    {
        var shortLinks = await _appDbContext.ShortLinks
                                            .Where(i => DateTime.Compare(i.CreateDate, DateTime.Now) <= 2 /*ExpiryDays*/)
                                            .Include(i => i.User)
                                            .ToListAsync();
        return shortLinks;
    }

    public async Task<ShortLink> GetByIdAsync(decimal id)
    {
        var userFound = await _appDbContext.ShortLinks.Where(i => i.Id == id)
                                                    .Include(i => i.User)
                                                    .FirstOrDefaultAsync();
        return userFound;
    }
    public async Task<ShortLink> GetByUniqueCodeAsync(string uniqueCode, CancellationToken cancellationToken)
    {
        return await _appDbContext.ShortLinks.FirstOrDefaultAsync(i => i.UniqueCode == uniqueCode && !i.IsExpired, cancellationToken);
    }
    public async Task<ShortLink> GetByOriginUrlAsync(string originUrl, CancellationToken cancellationToken)
    {
        return await _appDbContext.ShortLinks.FirstOrDefaultAsync(i => i.OriginUrl == originUrl, cancellationToken);
    }
}
