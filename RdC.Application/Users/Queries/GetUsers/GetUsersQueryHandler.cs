using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Role;
using RdC.Domain.DTO.User;

namespace RdC.Application.Users.Queries.GetUsers
{
    internal sealed class GetUsersQueryHandler
        : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            var usersResponse = users.Select(user => new UserResponse(
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
                user.CreatedAt)).ToList();

            return usersResponse;
        }
    }
}
