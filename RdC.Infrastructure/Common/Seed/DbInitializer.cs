using Microsoft.EntityFrameworkCore;
using RdC.Domain.Users;
using RdC.Infrastructure.Common.Persistance;

namespace RdC.Infrastructure.Common.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedAdministratorRoleAndUserAsync(RecouvrementDBContext context)
        {
            // Ensure database is created
            await context.Database.MigrateAsync();

            // Seed role
            var adminRole = await context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == "Administrateur");

            if (adminRole is null)
            {
                adminRole = Role.Create("Administrateur");
                context.Roles.Add(adminRole);
                await context.SaveChangesAsync();

                var permissions = await context.PermissionDefinitions.ToListAsync();
                foreach (var permission in permissions)
                {
                    var rolePermission = new RolePermission(
                        id: 0,
                        permissionDefinitionID: permission.Id,
                        roleID: adminRole.Id,
                        canRead: true,
                        canWrite: true,
                        canCreate: true
                    );
                    context.RolePermissions.Add(rolePermission);
                }

                await context.SaveChangesAsync();
            }

            // Seed administrator
            if (!context.Users.Any())
            {
                var adminEmail = "moataz.achour01@gmail.com";
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("admin");

                var user = User.CreateAdminUser(
                    adminEmail,
                    hashedPassword,
                    adminRole.Id);

                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
