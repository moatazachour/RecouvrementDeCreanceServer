using RdC.Application;
using RdC.Infrastructure;
using RdC.WorkerService;

var builder = Host.CreateApplicationBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services
    .AddApplication()
    .AddInfrastructure(connectionString);


builder.Services.AddHostedService<PaymentReminderService>();

var host = builder.Build();
host.Run();