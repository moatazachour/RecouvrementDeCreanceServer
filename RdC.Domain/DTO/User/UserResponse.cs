using RdC.Domain.DTO.Role;
using RdC.Domain.Users;

namespace RdC.Domain.DTO.User
{
    public record UserResponse(
        int UserID,
        string? username,
        string email,
        UserStatus userStatus,
        RoleResponse role,
        DateTime createdAt);
}
