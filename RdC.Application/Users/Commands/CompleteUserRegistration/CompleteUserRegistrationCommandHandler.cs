using MediatR;
using MediatR.Wrappers;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Application.Common.Security;

namespace RdC.Application.Users.Commands.CompleteUserRegistration
{
    internal sealed class CompleteUserRegistrationCommandHandler
        : IRequestHandler<CompleteUserRegistrationCommand, bool>
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

        public async Task<bool> Handle(CompleteUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.userEmail);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await _userRepository.IsUsernameExistAsync(request.username))
            {
                return false;
            }

            string hashedPassword = _passwordHasher.Hash(request.password);

            user.ContinueRegistration(
                request.username,
                hashedPassword);


            await _unitOfWork.CommitChangesAsync();

            await _domainEventDispatcher.DispatchEventsAsync(user);

            return true;
        }
    }
}
