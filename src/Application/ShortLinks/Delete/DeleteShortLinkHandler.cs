using Application.Results;
using Domain.Entities.ShortLinkAggregate;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.Delete;

public class DeleteShortLinkHandler(
    IUnitOfWork _unitOfWork,
    IShortLinkRepository _shortLinkRepository) :
    IRequestHandler<DeleteShortLinkCommand, Result<bool>>

{
    public async Task<Result<bool>> Handle(DeleteShortLinkCommand request, CancellationToken cancellationToken)
    {
        await _shortLinkRepository.Delete(request.Id);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure(Errors.DeleteError);
    }
}