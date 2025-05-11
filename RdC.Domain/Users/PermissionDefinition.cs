using RdC.Domain.Abstrations;

namespace RdC.Domain.Users
{
    public sealed class PermissionDefinition : Entity
    {
        public PermissionDefinition(
            int id,
            string permissionName)
            : base(id)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; set; }
        
        private PermissionDefinition() { }
    }
}
