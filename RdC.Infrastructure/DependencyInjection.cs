﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RdC.Application.Common.Email;
using RdC.Application.Common.Interfaces;
using RdC.Application.Common.Pdf;
using RdC.Application.Common.Security;
using RdC.Infrastructure.Acheteurs.Persistance;
using RdC.Infrastructure.Common.Persistance;
using RdC.Infrastructure.Email;
using RdC.Infrastructure.Factures.Persistance;
using RdC.Infrastructure.Litiges.Persistance;
using RdC.Infrastructure.PaiementDates.Persistance;
using RdC.Infrastructure.Paiements.Persistance;
using RdC.Infrastructure.Pdf;
using RdC.Infrastructure.Permissions.Persistance;
using RdC.Infrastructure.PlanDePaiements.Persistance;
using RdC.Infrastructure.Relances.Persistance.Email;
using RdC.Infrastructure.Relances.Persistance.SMS;
using RdC.Infrastructure.Roles.Persistance;
using RdC.Infrastructure.Security;
using RdC.Infrastructure.Users.Persistance;

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
            services.AddScoped<ISMSRelanceRepository, SMSRelanceRepository>();
            services.AddScoped<ILitigeRepository, LitigeRepository>();
            services.AddScoped<ILitigeTypeRepository, LitigeTypeRepository>();
            services.AddScoped<ILitigeJustificatifRepository, LitigeJustificatifRepository>();
            services.AddScoped<IPermissionDefinitionRepository, PermissionDefinitionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPdfGeneratorService, PdfGeneratorService>();
            services.AddTransient<IQuestPdfSignatureVerifier, QuestPdfSignatureVerifier>();

            services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();

            return services;
        }
    }
}
