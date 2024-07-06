using Domain.Entities.UserAggregate;
using Domain.Interfaces.Repository.Users;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories.Users;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(User user)
    {
        await _appDbContext.Users.AddAsync(user);
    }

    public async Task<bool> Delete(decimal id)
    {
        var userFound = await GetByIdAsync(id);
        if (userFound == null)
            return false;
        
        _appDbContext.Users.Remove(userFound);
        return true;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var userFound = await GetByIdAsync(user.Id);
        if (userFound == null)
            return false;

        userFound = user;
        _appDbContext.Set<User>().Attach(user);
        _appDbContext.Set<User>().Entry(userFound).State = EntityState.Modified;

        return true;
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