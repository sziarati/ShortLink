using Domain.Entities.UserAggregate;

namespace Domain.Interfaces.Repository.Users;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> Delete(decimal id);
    Task<bool> UpdateAsync(User user);

    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(decimal id);
}
