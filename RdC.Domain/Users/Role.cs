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

        public string RoleName { get; set; }
        public List<RolePermission> RolePermissions { get; set; } = new();
        public List<User> Users { get; private set; } = new();

        public static Role Create(
            string roleName)
        {
            return new Role(
                id: 0,
                roleName: roleName);
        }

        private Role() { }
    }
}
