using Application.Results;
using Application.Users.Delete;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.Users;
using MediatR;

namespace Application.Users.Handlers;

public class DeleteUserHandler(
    IUnitOfWork _unitOfWork, 
    IUserRepository _userRepository) :

    IRequestHandler<DeleteUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(request.Id);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure(Errors.DeleteError);
    }
}