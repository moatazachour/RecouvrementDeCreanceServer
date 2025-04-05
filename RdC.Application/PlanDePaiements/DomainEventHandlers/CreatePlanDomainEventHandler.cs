using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Domain.PaiementDates.Events;
using RdC.Domain.PlanDePaiements.Events;

namespace RdC.Application.PlanDePaiements.DomainEventHandlers
{
    internal sealed class CreatePlanDomainEventHandler : INotificationHandler<CreatePlanDomainEvent>
    {
        private IPlanDePaiementRepository _planDePaiementRepository;
        private IPaiementDateRepository _paiementDateRepository;
        private IUnitOfWork _unitOfWork;
        private IDomainEventDispatcher _domainEventDispatcher;

        public CreatePlanDomainEventHandler(
            IPlanDePaiementRepository planDePaiementRepository, 
            IPaiementDateRepository paiementDateRepository, 
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _paiementDateRepository = paiementDateRepository;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task Handle(CreatePlanDomainEvent notification, CancellationToken cancellationToken)
        {
            var plan = await _planDePaiementRepository.GetByIdAsync(notification.PlanID);

            var listPaiementDates = new List<PaiementDate>();

            if (plan is null)
                return;
            
            int nombreDeEcheances = plan.NombreDeEcheances;

            var paiementDates = new List<DateOnly>();

            for (int i = 0; i < nombreDeEcheances; i++)
            {
                listPaiementDates.Add(PaiementDate.Create(
                    notification.PlanID,
                    DateTime.Now.AddMonths(i + 1),
                    plan.MontantDeChaqueEcheance));
            }

            await _paiementDateRepository.AddAsync(listPaiementDates);

            await _unitOfWork.CommitChangesAsync();

            listPaiementDates[0].RaiseDomainEvent(new CreatePaiementDatesDomainEvent(notification.PlanID));

            await _domainEventDispatcher.DispatchEventsAsync(listPaiementDates[0]);
        }
    }
}
