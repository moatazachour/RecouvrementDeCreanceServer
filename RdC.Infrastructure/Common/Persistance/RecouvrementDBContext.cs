using Microsoft.EntityFrameworkCore;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Acheteurs;
using RdC.Domain.Factures;
using RdC.Domain.Litiges;
using RdC.Domain.PaiementDates;
using RdC.Domain.Paiements;
using RdC.Domain.PlanDePaiements;
using RdC.Domain.Relances;
using RdC.Domain.Users;
using System.Reflection;

namespace RdC.Infrastructure.Common.Persistance
{
    public class RecouvrementDBContext : DbContext, IUnitOfWork
    {
        public DbSet<Acheteur> Acheteurs { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<PlanDePaiement> PlanDePaiements { get; set; }
        public DbSet<PaiementDate> PaiementDates { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Relance> Relances { get; set; }
        public DbSet<EmailRelance> EmailRelances { get; set; }
        public DbSet<SMSRelance> SMSRelances { get; set; }
        public DbSet<Litige> Litiges { get; set; }
        public DbSet<LitigeType> LitigeTypes { get; set; }
        public DbSet<LitigeJustificatif> Justificatifs { get; set; }
        public DbSet<PermissionDefinition> PermissionDefinitions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public RecouvrementDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public async Task CommitChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}