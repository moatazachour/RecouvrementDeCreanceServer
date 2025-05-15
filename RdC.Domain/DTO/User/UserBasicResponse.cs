using RdC.Domain.Users;

namespace RdC.Domain.DTO.User
{
    public record UserBasicResponse(
        int UserID,
        string? username,
        string email,
        UserStatus userStatus,
        DateTime createdAt);
}
