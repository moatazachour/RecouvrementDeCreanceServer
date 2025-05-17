using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Abstrations;
using RdC.Domain.Users;

namespace RdC.Application.Users.Commands.ReactivateUser
{
    internal sealed class ReactivateUserCommandHandler
        : IRequestHandler<ReactivateUserCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReactivateUserCommandHandler(
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ReactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userID);

            if (user is null)
            {
                return Result<bool>.Failure($"User with ID {request.userID} don't exist.");
            }

            if (user.Status == UserStatus.ACTIVE)
            {
                return Result<bool>.Failure($"User already active.");
            }

            if (user.Status == UserStatus.EN_ATTENTE)
            {
                return Result<bool>.Failure($"User is not fully registred.");
            }

            user.Reactivate();

            await _unitOfWork.CommitChangesAsync();

            return Result<bool>.SuccessResult(true);
        }
    }
}
