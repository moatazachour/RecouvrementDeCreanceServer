using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Infrastructure.Acheteurs.Persistance;
using RdC.Infrastructure.Common.Persistance;
using RdC.Infrastructure.Email;
using RdC.Infrastructure.Factures.Persistance;
using RdC.Infrastructure.PaiementDates.Persistance;
using RdC.Infrastructure.Paiements.Persistance;
using RdC.Infrastructure.PlanDePaiements.Persistance;

namespace RdC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<RecouvrementDBContext>(options =>
                    options.UseSqlServer("""
                            Server=localhost;
                            Database=RdC;
                            User Id=sa;
                            Password=sa123456;
                            TrustServerCertificate=True;
                        """));

            services.AddHttpClient<AcheteurRepository>();
            services.AddHttpClient<FactureRepository>();

            services.AddScoped<IUnitOfWork>(serviceProvider =>
                serviceProvider.GetRequiredService<RecouvrementDBContext>());

            services.AddScoped<IAcheteurRepository, AcheteurRepository>();
            services.AddScoped<IFactureRepository, FactureRepository>();
            services.AddScoped<IPlanDePaiementRepository, PlanDePaiementRepository>();
            services.AddScoped<IPaiementDateRepository, PaiementDateRepository>();
            services.AddScoped<IPaiementRepository, PaiementRepository>();

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
