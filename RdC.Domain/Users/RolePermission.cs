using RdC.Domain.Abstrations;

namespace RdC.Domain.Users
{
    public sealed class RolePermission : Entity
    {
        public RolePermission(
            int id,
            int permissionDefinitionID,
            int roleID,
            bool canRead = false,
            bool canWrite = false,
            bool canCreate = false)
            : base(id)
        {
            PermissionDefinitionID = permissionDefinitionID;
            RoleID = roleID;
            CanRead = canRead;
            CanWrite = canWrite;
            CanCreate = canCreate;
        }

        public int PermissionDefinitionID { get; private set; }
        public PermissionDefinition permissionDefinition { get; private set; }

        public int RoleID { get; private set; }
        public Role Role {  get; private set; }

        public bool CanRead { get; private set; }
        public bool CanWrite { get; private set; }
        public bool CanCreate { get; private set; }

        private RolePermission() { }
    }
}
