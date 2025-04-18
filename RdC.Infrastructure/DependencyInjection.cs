using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Infrastructure.Acheteurs.Persistance;
using RdC.Infrastructure.Common.Persistance;
using RdC.Infrastructure.Email;
using RdC.Infrastructure.Factures.Persistance;
using RdC.Infrastructure.Litiges.Persistance;
using RdC.Infrastructure.PaiementDates.Persistance;
using RdC.Infrastructure.Paiements.Persistance;
using RdC.Infrastructure.PlanDePaiements.Persistance;
using RdC.Infrastructure.Relances.Persistance.Email;

namespace RdC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<RecouvrementDBContext>(options =>
                    options.UseSqlServer(connectionString));

            services.AddHttpClient<AcheteurRepository>();
            services.AddHttpClient<FactureRepository>();

            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<RecouvrementDBContext>());

            services.AddScoped<IAcheteurRepository, AcheteurRepository>();
            services.AddScoped<IFactureRepository, FactureRepository>();
            services.AddScoped<IPlanDePaiementRepository, PlanDePaiementRepository>();
            services.AddScoped<IPaiementDateRepository, PaiementDateRepository>();
            services.AddScoped<IPaiementRepository, PaiementRepository>();
            services.AddScoped<IEmailRelanceRepository, EmailRelanceRepository>();
            services.AddScoped<ILitigeRepository, LitigeRepository>();
            services.AddScoped<ILitigeTypeRepository, LitigeTypeRepository>();

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
