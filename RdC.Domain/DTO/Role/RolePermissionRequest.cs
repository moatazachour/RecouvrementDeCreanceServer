namespace RdC.Domain.DTO.Role
{
    public record RolePermissionRequest(
        int permissionDefinitionID,
        bool canRead,
        bool canWrite,
        bool canCreate);
}
