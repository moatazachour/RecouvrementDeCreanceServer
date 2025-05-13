using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Role;
using RdC.Domain.DTO.User;

namespace RdC.Application.Users.Queries.GetUser
{
    internal sealed class GetUserQueryHandler
        : IRequestHandler<GetUserQuery, UserResponse?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userID);

            if (user is null)
            {
                return null;
            }


            var rolePermissions = user.Role.RolePermissions.Select(rp => new RolePermissionResponse(
            rp.Id,
            new PermissionDefinitionResponse(
                rp.PermissionDefinitionID,
                rp.PermissionDefinition.PermissionName),
            rp.CanRead,
            rp.CanWrite,
            rp.CanCreate)).ToList();



            return new UserResponse(
                user.Id,
                user.Username,
                user.Email,
                user.Status,
                new RoleResponse(user.Role.Id, user.Role.RoleName, rolePermissions),
                user.CreatedAt);
        }
    }
}
