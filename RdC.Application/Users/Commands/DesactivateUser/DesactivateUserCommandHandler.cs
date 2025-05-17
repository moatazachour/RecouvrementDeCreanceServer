using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Abstrations;
using RdC.Domain.Users;
using System.Runtime.CompilerServices;

namespace RdC.Application.Users.Commands.DesactivateUser
{
    internal sealed class DesactivateUserCommandHandler
        : IRequestHandler<DesactivateUserCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DesactivateUserCommandHandler(
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DesactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userID);

            if (user is null)
            {
                return Result<bool>.Failure($"User with ID {request.userID} don't exist.");
            }

            if (user.Status != UserStatus.ACTIVE)
            {
                return Result<bool>.Failure($"User already inactive.");
            }

            user.Desactivate();

            await _unitOfWork.CommitChangesAsync();

            return Result<bool>.SuccessResult(true);
        }
    }
}
