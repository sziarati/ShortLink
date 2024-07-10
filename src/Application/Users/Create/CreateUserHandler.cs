using Application.Results;
using Application.Users.Create;
using Domain.Entities.UserAggregate;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.Users;
using MediatR;

namespace Application.Users.Handlers;

public class CreateUserHandler(
    IUnitOfWork _unitOfWork, 
    IUserRepository _userRepository) :

    IRequestHandler<CreateUserCommand, Result<User>>
{
    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.UserName, request.Email, request.Password, request.Address);
        await _userRepository.AddAsync(user);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? Result<User>.Success(user) : Result<User>.Failure(Errors.CreationError);
    }

}