using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RdC.Application.Common.Interfaces;
using RdC.Infrastructure.Acheteurs.Persistance;
using RdC.Infrastructure.Common.Persistance;
using RdC.Infrastructure.Factures.Persistance;

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

            services.AddScoped<IAcheteurRepository, AcheteurRepository>();
            services.AddScoped<IFactureRepository, FactureRepository>();

            return services;
        }
    }
}
