﻿using Domain.Entities.ShortLinkAggregate;

namespace Domain.Interfaces.Repository.ShortLinks;

public interface IShortLinkRepository
{
    Task AddAsync(ShortLink shortLink);
    Task<bool> Delete(decimal id);

    Task<List<ShortLink>> GetAllAsync();
    Task<IReadOnlyList<ShortLink>> GetAllExpiredShortLinksAsync();
    Task<ShortLink> GetByIdAsync(decimal id);
    Task<ShortLink> GetByUniqueCodeAsync(string uniqueCode, CancellationToken cancellationToken);
    Task<ShortLink> GetByOriginUrlAsync(string originUrl, CancellationToken cancellationToken);
    Task<bool> SetExpired(int id);
}
