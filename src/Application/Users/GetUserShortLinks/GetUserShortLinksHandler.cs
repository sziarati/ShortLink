using Domain.Entities.ShortLinkAggregate;
using Domain.Interfaces.Repository.Users;
using MediatR;

namespace Application.Users.GetUserShortLinks;

public class GetUserShortLinksHandler(IUserRepository _userRepository) :
    IRequestHandler<GetUserShortLinksQuery, List<ShortLink>>
{
    public async Task<List<ShortLink>> Handle(GetUserShortLinksQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserShortLinksAsync(request.UserName, cancellationToken);
    }
}
