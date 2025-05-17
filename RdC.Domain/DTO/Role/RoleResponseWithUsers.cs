using RdC.Domain.DTO.User;

namespace RdC.Domain.DTO.Role
{
    public record RoleResponseWithUsers(
        int roleID,
        string roleName,
        List<RolePermissionResponse> rolePermissionResponses,
        List<UserBasicResponse> roleUsers);
}
