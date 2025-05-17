using MediatR;
using MediatR.Wrappers;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Application.Common.Security;
using RdC.Domain.Abstrations;

namespace RdC.Application.Users.Commands.CompleteUserRegistration
{
    internal sealed class CompleteUserRegistrationCommandHandler
        : IRequestHandler<CompleteUserRegistrationCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public CompleteUserRegistrationCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork, 
            IDomainEventDispatcher domainEventDispatcher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<Result<bool>> Handle(CompleteUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.userEmail);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Status != Domain.Users.UserStatus.EN_ATTENTE)
            {
                return Result<bool>.Failure("Account already registred.");
            }

            if (await _userRepository.IsUsernameExistAsync(request.username))
            {
                return Result<bool>.Failure("Username exist in the system.");
            }

            string hashedPassword = _passwordHasher.Hash(request.password);

            user.ContinueRegistration(
                request.username,
                hashedPassword);


            await _unitOfWork.CommitChangesAsync();

            await _domainEventDispatcher.DispatchEventsAsync(user);

            return Result<bool>.SuccessResult(true);
        }
    }
}
