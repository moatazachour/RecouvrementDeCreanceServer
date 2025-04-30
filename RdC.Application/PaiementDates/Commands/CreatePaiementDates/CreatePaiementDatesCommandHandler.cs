using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Application.Paiements.Commands.CreatePaiement;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.PaiementDates;
using RdC.Domain.PaiementDates.Events;
using RdC.Domain.Paiements;
using System.Threading.Tasks;

namespace RdC.Application.PaiementDates.Commands.CreatePaiementDates
{
    internal sealed class CreatePaiementDatesCommandHandler
        : IRequestHandler<CreatePaiementDatesCommand, Unit>
    {
        private readonly IPaiementDateRepository _paiementDateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ISender _mediator;

        public CreatePaiementDatesCommandHandler(
            IPaiementDateRepository paiementDateRepository, 
            IUnitOfWork unitOfWork,
            IDomainEventDispatcher domainEventDispatcher,
            ISender mediator)
        {
            _paiementDateRepository = paiementDateRepository;
            _unitOfWork = unitOfWork;
            _domainEventDispatcher = domainEventDispatcher;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreatePaiementDatesCommand request, CancellationToken cancellationToken)
        {
            var paiementDates = new List<PaiementDate>();

            foreach (var paiementDate in request.createPaiementDatesRequest.PaiementDates)
            {
                paiementDates.Add(PaiementDate.Create(
                    paiementDate.PlanID,
                    paiementDate.EcheanceDate,
                    paiementDate.MontantDeEcheance));
            }

            await _paiementDateRepository.AddAsync(paiementDates);

            await _unitOfWork.CommitChangesAsync();

            paiementDates[0].RaiseDomainEvent(new CreatePaiementDatesDomainEvent(paiementDates[0].PlanID));

            await _domainEventDispatcher.DispatchEventsAsync(paiementDates[0]);

            return Unit.Value;
        }
    }
}
