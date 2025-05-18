using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.Users.Commands.UpdateUserRole
{
    internal sealed class UpdateUserRoleCommandHandler
        : IRequestHandler<UpdateUserRoleCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserRoleCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userID);

            if (user is null)
            {
                return false;
            }

            user.ChangeRole(request.roleID);

            await _unitOfWork.CommitChangesAsync();

            return true;
        }
    }
}
