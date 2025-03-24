using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PaiementDates;
using RdC.Domain.PaiementDates.Events;
using RdC.Domain.PlanDePaiements.Events;

namespace RdC.Application.PlanDePaiements.Events
{
    internal sealed class CreatePlanDomainEventHandler : INotificationHandler<CreatePlanDomainEvent>
    {
        private IPlanDePaiementRepository _planDePaiementRepository;
        private IPaiementDateRepository _paiementDateRepository;
        private IUnitOfWork _unitOfWork;

        public CreatePlanDomainEventHandler(
            IPlanDePaiementRepository planDePaiementRepository, 
            IPaiementDateRepository paiementDateRepository, 
            IUnitOfWork unitOfWork)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _paiementDateRepository = paiementDateRepository;
            _unitOfWork = unitOfWork;
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
                    DateTime.Now.AddMonths(i + 1)));
            }

            await _paiementDateRepository.AddAsync(listPaiementDates);

            await _unitOfWork.CommitChangesAsync();

            listPaiementDates[0].RaiseDomainEvent(new CreatePaiementDatesDomainEvent(notification.PlanID));
        }
    }
}
