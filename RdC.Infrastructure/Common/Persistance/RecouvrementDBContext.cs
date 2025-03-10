using Microsoft.EntityFrameworkCore;
using RdC.Domain.Acheteurs;
using RdC.Domain.Factures;
using System.Reflection;

namespace RdC.Infrastructure.Common.Persistance
{
    public class RecouvrementDBContext : DbContext
    {
        public DbSet<Acheteur> Acheteurs { get; set; }
        public DbSet<Facture> Factures { get; set; }

        public RecouvrementDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}