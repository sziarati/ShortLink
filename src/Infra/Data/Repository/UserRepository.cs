using Domain.Entities.ShortLinkAggregate;
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
    public async Task<List<User>> GetAllAsync()
    {
        return await _appDbContext.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(decimal id)
    {
        var userFound = await _appDbContext.Users.Where(i => i.Id == id)
                                                 .Include(i => i.ShortLinks)                                                
                                                 .FirstOrDefaultAsync();
        return userFound;
    }

}