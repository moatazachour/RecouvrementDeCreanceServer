using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Application.Common.Security;
using RdC.Domain.Abstrations;
using RdC.Domain.DTO.Role;
using RdC.Domain.DTO.User;
using RdC.Domain.Users;

namespace RdC.Application.Users.Commands.Login
{
    internal sealed class LoginCommandHandler
        : IRequestHandler<LoginCommand, Result<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdentifier(request.Identifier.Trim());

            if (user is null)
            {
                return Result<UserResponse>.Failure("Wrong Identifier.");
            }

            if (user.Status == UserStatus.EN_ATTENTE)
            {
                return Result<UserResponse>.Failure("Registration not completed. Please check your email.");
            }

            if (user.Status == UserStatus.INACTIVE)
            {
                return Result<UserResponse>.Failure("Account is inactive. Contact administrator.");
            }


            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Result<UserResponse>.Failure("Incorrect password.");
            }

            var userResponse = new UserResponse(
                user.Id,
                user.Username,
                user.Email,
                user.Status,
                new RoleResponse(
                    user.Role.Id,
                    user.Role.RoleName,
                    user.Role.RolePermissions.Select(rp => new RolePermissionResponse(
                        rp.Id,
                        new PermissionDefinitionResponse(
                            rp.PermissionDefinitionID,
                            rp.PermissionDefinition.PermissionName),
                        rp.CanRead,
                        rp.CanWrite,
                        rp.CanCreate)).ToList()),
                user.CreatedAt);

            return Result<UserResponse>.SuccessResult(userResponse);
        }
    }
}
