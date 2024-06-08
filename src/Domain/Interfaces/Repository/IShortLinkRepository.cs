using Domain.Entities.ShortLinkAggregate;

namespace Domain.Interfaces.Repository;

public interface IShortLinkRepository
{
    Task<decimal> AddAsync(ShortLink shortLink);
    Task<bool> Delete(decimal id);

    Task<List<ShortLink>> GetAllAsync();
    Task<IReadOnlyList<ShortLink>> GetAllExpiredShortLinksAsync();
    Task<ShortLink> GetByIdAsync(decimal id);
    Task<ShortLink?> GetShortLinkAsync(string uniqueCode);
}
