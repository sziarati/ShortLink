using Domain.Entities.UserAggregate;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<decimal> AddAsync(User user)
    {        
        await _appDbContext.Users.AddAsync(user);
        var result = await _appDbContext.SaveChangesAsync();

        return result > 0 ? user.Id : default;
    }

    public async Task<bool> Delete(decimal id)
    {
        var userFound = await GetByIdAsync(id);
        if (userFound != null)
            _appDbContext.Users.Remove(userFound);

        var result = await _appDbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var userFound = await GetByIdAsync(user.Id);
        if (userFound == null)
            return false;

        userFound = user;
        _appDbContext.Entry(userFound).State = EntityState.Modified;
        var result = await _appDbContext.SaveChangesAsync();
        return result > 0;
    }
    public async Task AddShortLinkAsync(decimal userId, string originUrl, DateTime expireDate)
    {
        var user = await GetByIdAsync(userId);
        if (user != null)
        {
            user.AddShortLink(originUrl, expireDate);
            await UpdateAsync(user);
        }
    }

    public async Task RemoveShortLinkAsync(decimal userId, decimal shortLinkId)
    {
        var user = await GetByIdAsync(userId);
        if (user != null)
        {
            user.RemoveShortLink(shortLinkId);
            await UpdateAsync(user);
        }
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _appDbContext.Users.ToListAsync();
    }

    public async Task<IReadOnlyList<ShortLink>> GetAllShortLinksAsync()
    {
        var users = await _appDbContext.Users.Include(i => i.ShortLinks).ToListAsync();
        List<ShortLink> shortLinks = new ();
        users.ForEach(i => shortLinks.AddRange(i.ShortLinks));
        return shortLinks;
    }

    public async Task<IReadOnlyList<ShortLink>> GetAllExpiredShortLinksAsync()
    {
        var users = await _appDbContext.Users.Include(i => i.ShortLinks)
                                             .Where(i => i.ShortLinks != null && i.ShortLinks.Any(i => DateTime.Compare(i.ExpireDate, DateTime.Now) < 0 ))
                                             .ToListAsync();
        List<ShortLink> shortLinks = new();
        users.ForEach(i => shortLinks.AddRange(i.ShortLinks));
        return shortLinks;
    }
    public async Task<User> GetByIdAsync(decimal id)
    {
        var userFound = await _appDbContext.Users.Where(i => i.Id == id)
                                                 .Include(i => i.ShortLinks)                                                
                                                 .FirstOrDefaultAsync();
        return userFound;
    }

    public async Task<ShortLink?> GetShortLinkAsync(decimal id)
    {
        var user = await _appDbContext.Users.Include(i => i.ShortLinks)
                                             .Where(i => i.ShortLinks != null && i.ShortLinks.Any(i => i.Id == id))
                                             .FirstOrDefaultAsync();

        if (user?.ShortLinks?.Count > 0)
            return user.ShortLinks.FirstOrDefault(i => i.Id == id);

        return null;
    }
}