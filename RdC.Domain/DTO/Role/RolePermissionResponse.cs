namespace RdC.Domain.DTO.Role
{
    public record RolePermissionResponse(
        int id,
        PermissionDefinitionResponse permissionDefinition,
        bool canRead,
        bool canWrite,
        bool canCreate);
}
