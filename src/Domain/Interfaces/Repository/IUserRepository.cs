using Domain.Entities.ShortLinkAggregate;
using Domain.Entities.UserAggregate;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task<decimal> AddAsync(User user);
    Task<bool> Delete(decimal id);
    Task<bool> UpdateAsync(User user);

    Task<List<User>> GetAllAsync();
    Task<IReadOnlyList<ShortLink>> GetAllShortLinksAsync();
    Task<IReadOnlyList<ShortLink>> GetAllExpiredShortLinksAsync();
    Task<User> GetByIdAsync(decimal id);
}
