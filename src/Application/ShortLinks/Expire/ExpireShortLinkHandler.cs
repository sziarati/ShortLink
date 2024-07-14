using Application.Results;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.Expire;

public class ExpireShortLinkHandler(
    IUnitOfWork _unitOfWork,
    IShortLinkRepository _shortLinkRepository) :

    IRequestHandler<ExpireShortLinkCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ExpireShortLinkCommand request, CancellationToken cancellationToken)
    {
        await _shortLinkRepository.SetExpired(request.id);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure(Errors.ExpireShortLinkError);
    }
}