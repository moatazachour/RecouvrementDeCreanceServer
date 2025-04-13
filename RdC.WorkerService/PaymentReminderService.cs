using MediatR;
using RdC.Application.PaiementDates.Queries.GetTodaysPaiementDates;
using RdC.Application.PlanDePaiements.Commands.CheckPlanStatus;
using RdC.Application.Relances.Commands.SendRelance;
using RdC.Domain.PaiementDates;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RdC.WorkerService
{
    public class PaymentReminderService : BackgroundService
    {
        private readonly ILogger<PaymentReminderService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;


        public PaymentReminderService(
            ILogger<PaymentReminderService> logger,
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service starting");
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var listOfTodaysUnpaidPaiementDates = await _GetTodaysPaiementDates();

                if (listOfTodaysUnpaidPaiementDates.Count == 0)
                {
                    _logger.LogInformation("No unpaid payments found for today.");
                    break;
                }

                if (listOfTodaysUnpaidPaiementDates.Count > 0)
                {
                    foreach (var paiementDate in listOfTodaysUnpaidPaiementDates)
                    {
                        if (await _CheckIfPlanStillActive(paiementDate))
                        {
                            await _SendPaiementRemainder(paiementDate.Id);
                        }
                    } 
                }

                _logger.LogInformation("Payment reminder task completed, waiting for the next execution.");
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task<List<PaiementDate>> _GetTodaysPaiementDates()
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var listOfTodaysPaiementDates = await mediator.Send(new GetTodaysPaiementDatesQuery());
            return listOfTodaysPaiementDates;
        }

        private async Task<bool> _CheckIfPlanStillActive(PaiementDate paiementDate)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            int maxUnpaidPaiements = _configuration.GetValue<int>("PlanSettings:MaxUnpaidPaiements");

            var command = new CheckPlanStatusCommand(paiementDate.PlanID, maxUnpaidPaiements);

            return await mediator.Send(command);
        }

        private async Task _SendPaiementRemainder(int PaiementDateID)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var command = new SendRelanceCommand(PaiementDateID);

            var isSent = await mediator.Send(command);

            _logger.LogInformation(isSent.ToString());
        }
    }
}
