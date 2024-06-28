using Domain.Interfaces.Repository.ShortLinks;
using Domain.Interfaces.Repository.Users;

namespace Domain.Interfaces.Repository;

public interface IUnitOfWork
{
    public IShortLinkRepository _shortLinkRepository { get; }
    public IUserRepository _userRepository { get; }
    public Task<int> SaveChangesAsync();
}
