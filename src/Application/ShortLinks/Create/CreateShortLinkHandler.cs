using Application.Results;
using Domain.Entities.ShortLinkAggregate;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.ShortLinks;
using MediatR;

namespace Application.ShortLinks.Create;

public class CreateShortLinkHandler(
    IUnitOfWork _unitOfWork,
    IShortLinkRepository _shortLinkRepository) :

    IRequestHandler<CreateShortLinkCommand, Result<ShortLink>>
{
    public async Task<Result<ShortLink>> Handle(CreateShortLinkCommand request, CancellationToken cancellationToken)
    {
        var shortLink = new ShortLink(request.Name, request.OriginUrl, request.UserId);
        await _shortLinkRepository.AddAsync(shortLink);
        var result = await _unitOfWork.SaveChangesAsync();
        
        return result > 0 ? 
            Result<ShortLink>.Success(shortLink): 
            Result<ShortLink>.Failure(Errors.CreationError);
    }
}