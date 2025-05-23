﻿using RdC.Domain.Abstrations;
using RdC.Domain.Litiges;
using RdC.Domain.Paiements;
using RdC.Domain.PlanDePaiements;
using RdC.Domain.Users.Events;

namespace RdC.Domain.Users
{
    public sealed class User : Entity
    {
        private User(
            int id,
            string? username,
            string email,
            string? passwordHash,
            UserStatus userStatus,
            int roleID,
            DateTime createdAt)
            : base(id)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Status = userStatus;
            RoleID = roleID;
            CreatedAt = createdAt;
        }

        public string? Username { get; private set; }
        public string Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public UserStatus Status { get; private set; }

        public int RoleID { get; private set; }
        public Role Role { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public List<PlanDePaiement> planDePaiements { get; private set; } = new();

        public List<Litige> litiges { get; private set; } = new();

        public List<Paiement> paiements { get; private set; } = new();

        public static User CreateAdminUser(
            string username,
            string email,
            string passwordHash,
            int roleID)
        {
            var user = new User(
                id: 0,
                username: username,
                email: email,
                passwordHash: passwordHash,
                userStatus: UserStatus.ACTIVE,
                roleID: roleID,
                createdAt: DateTime.Now);

            return user;
        }

        public static User Create(
            string email,
            int roleID)
        {
            var user = new User(
                id: 0,
                username: null,
                email: email,
                passwordHash: null,
                userStatus: UserStatus.EN_ATTENTE,
                roleID: roleID,
                createdAt: DateTime.Now);

            return user;
        }

        public void ContinueRegistration(
            string username,
            string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;

            Status = UserStatus.ACTIVE;

            RaiseDomainEvent(new ContinueRegistrationDomainEvent(Id));
        }

        public void Desactivate()
        {
            Status = UserStatus.INACTIVE;
        }

        public void Reactivate()
        {
            Status = UserStatus.ACTIVE;
        }

        public void ChangeRole(int roleID)
        {
            RoleID = roleID;
        }

        private User() { }

    }
}
