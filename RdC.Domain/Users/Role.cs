using RdC.Domain.Abstrations;

namespace RdC.Domain.Users
{
    public sealed class Role : Entity
    {
        public Role(
            int id,
            string roleName)
            : base(id)
        {
            RoleName = roleName;
        }

        public string RoleName { get; private set; }
        public List<RolePermission> RolePermissions { get; private set; } = new();
        public List<User> Users { get; private set; } = new();

        public Role() { }
    }
}
