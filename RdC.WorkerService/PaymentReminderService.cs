using MediatR;
using RdC.Application.PaiementDates.Commands.CheckPreviousPaiement;
using RdC.Application.PaiementDates.Queries.GetPaiementDatesByOffset;
using RdC.Application.PaiementDates.Queries.GetPreviousPaiementDate;
using RdC.Application.PaiementDates.Queries.GetTodaysPaiementDates;
using RdC.Application.PlanDePaiements.Commands.CheckPlanStatus;
using RdC.Application.Relances.Commands.SendRelance;
using RdC.Domain.PaiementDates;
using RdC.Domain.Relances;
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
                await UpcomingUnpaidPaymentReminderBefore(3);
                await UpcomingUnpaidPaymentReminderBefore(1);

                await TodaysDateChecker();

                await OverdueUnpaidPaymentReminder(1);
                await OverdueUnpaidPaymentReminder(3);
                await OverdueUnpaidPaymentReminder(7);

                _logger.LogInformation("Payment reminder task completed, waiting for the next execution.");
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task UpcomingUnpaidPaymentReminderBefore(int days)
        {
            var futuresUnpaidPaiementDates = await _GetPaiementDatesByOffset(DaysOffset: days);

            if (futuresUnpaidPaiementDates.Count == 0)
                return;

            foreach (var unpaidPaiementDate in futuresUnpaidPaiementDates)
            {
                var previousPaiementDate = await _GetPreviousPaiementDate(unpaidPaiementDate.Id);

                if (previousPaiementDate != null && !previousPaiementDate.IsPaid)
                {
                    await _SendPaiementRemainder(
                        unpaidPaiementDate.Id,
                        RelanceContext.UpcomingPaymentReminderWithUnpaidPreviousPayment);

                    continue;
                }

                await _SendPaiementRemainder(unpaidPaiementDate.Id, RelanceContext.UpcomingPaymentReminder);
            }
        }

        private async Task TodaysDateChecker()
        {
            var todaysUnpaidPaiementDates = await _GetPaiementDatesByOffset(DaysOffset: 0);

            if (todaysUnpaidPaiementDates.Count == 0)
                return;

            foreach (var unpaidPaiementDate in todaysUnpaidPaiementDates)
            {
                if (!await _CheckIfPlanStillActive(unpaidPaiementDate))
                {
                    return;
                }

                await _CheckPreviousPaiement(unpaidPaiementDate.Id);
            }
        }

        private async Task OverdueUnpaidPaymentReminder(int days)
        {
            var futuresUnpaidPaiementDates = await _GetPaiementDatesByOffset((-1) * days);

            if (futuresUnpaidPaiementDates.Count == 0)
                return;

            foreach (var unpaidPaiementDate in futuresUnpaidPaiementDates)
            {
                await _SendPaiementRemainder(unpaidPaiementDate.Id, RelanceContext.OverduePaymentReminder);
            }
        }



        private async Task<List<PaiementDate>> _GetPaiementDatesByOffset(int DaysOffset)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var listPaiementDatesByOffset = await mediator.Send(new GetPaiementDatesByOffsetQuery(DaysOffset));
            return listPaiementDatesByOffset;
        }

        private async Task<PaiementDate?> _GetPreviousPaiementDate(int paiementDateID)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(new GetPreviousPaiementDateQuery(paiementDateID));
        }

        private async Task<bool> _CheckIfPlanStillActive(PaiementDate paiementDate)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            int maxUnpaidPaiements = _configuration.GetValue<int>("PlanSettings:MaxUnpaidPaiements");

            var command = new CheckPlanStatusCommand(paiementDate.PlanID, maxUnpaidPaiements);

            return await mediator.Send(command);
        }

        private async Task _SendPaiementRemainder(int PaiementDateID, RelanceContext relanceContext)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var command = new SendRelanceCommand(PaiementDateID, relanceContext);

            var isSent = await mediator.Send(command);

            _logger.LogInformation(isSent.ToString());
        }

        private async Task _CheckPreviousPaiement(int PaiementDateID)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var command = new CheckPreviousPaiementCommand(PaiementDateID);
            
            await mediator.Send(command);
        }

    }
}
