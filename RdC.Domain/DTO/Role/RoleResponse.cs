namespace RdC.Domain.DTO.Role
{
    public record RoleResponse(
        int roleID,
        string roleName,
        List<RolePermissionResponse> rolePermissionResponses);
}
