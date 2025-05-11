using RdC.Domain.Abstrations;
using RdC.Domain.PlanDePaiements;
using System.ComponentModel.DataAnnotations;

namespace RdC.Domain.Users
{
    public sealed class User : Entity
    {
        private User(
            int id,
            string email,
            string passwordHash,
            UserStatus userStatus,
            int roleID,
            DateTime createdAt)
            : base(id)
        {
            Email = email;
            PasswordHash = passwordHash;
            Status = userStatus;
            RoleID = roleID;
            CreatedAt = createdAt;
        }

        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserStatus Status { get; private set; }

        public int RoleID { get; private set; }
        public Role Role { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public List<PlanDePaiement> planDePaiements { get; private set; } = new();

        public static User CreateAdminUser(
            string email,
            string passwordHash,
            int roleID)
        {
            var user = new User(
                id: 0,
                email: email,
                passwordHash: passwordHash,
                userStatus: UserStatus.ACTIVE,
                roleID: roleID,
                createdAt: DateTime.Now);

            return user;
        }

        private User() { }
        
    }
}
