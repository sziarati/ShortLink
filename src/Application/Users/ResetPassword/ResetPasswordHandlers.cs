using Application.Notification;
using Application.Results;
using Application.Users.ResetPassword;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.Users;
using MediatR;

namespace Application.Users.Handlers;

public class ResetPasswordHandlers(
    IUnitOfWork _unitOfWork, 
    IUserRepository _userRepository,
    INotificationService _notificationService) :

    IRequestHandler<ResetPasswordCommand, Result<string>>

{
    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserNameAsync(request.UserName);
        user.ResetPassword(request.Password);
        await _userRepository.UpdateAsync(user);
        var result = await _unitOfWork.SaveChangesAsync();
        if (result > 0)
            await _notificationService.Notify(user.Email.Value, $"dear {user.UserName} your password changed!", NotificationType.Email);

        //expire Token for reLogin

        return result > 0 ? Result<string>.Success("") : Result<string>.Failure(Errors.InvalidPasswordError);
    }
}