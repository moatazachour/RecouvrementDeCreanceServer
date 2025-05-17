namespace RdC.Domain.DTO.Role
{
    public record RoleRequest(
        string roleName,
        List<RolePermissionRequest> rolePermission);
}
