using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Users;
using RdC.Domain.Users.Events;

namespace RdC.Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string email = request.email;
            
            if (await _userRepository.IsEmailExistAsync(email.Trim()))
            {
                return -1;
            }

            var user = User.Create(
                email,
                request.roleID);

            await _userRepository.AddAsync(user);

            await _unitOfWork.CommitChangesAsync();

            user.RaiseDomainEvent(new CreateUserDomainEvent(user.Id));

            await _domainEventDispatcher.DispatchEventsAsync(user);

            return user.Id;
        }
    }
}
