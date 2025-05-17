using MediatR;
using RdC.Application.Common.Dispatcher;
using RdC.Application.Common.Interfaces;
using RdC.Domain.PlanDePaiements;
using RdC.Domain.PlanDePaiements.Events;

namespace RdC.Application.PlanDePaiements.Commands.CreatePlan
{
    internal sealed class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, int>
    {
        private readonly IPlanDePaiementRepository _planDePaiementRepository;
        private readonly IFactureRepository _factureRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePlanCommandHandler(
            IPlanDePaiementRepository planDePaiementRepository,
            IPaiementDateRepository paiementDateRepository,
            IFactureRepository factureRepository,
            IUnitOfWork unitOfWork)
        {
            _planDePaiementRepository = planDePaiementRepository;
            _factureRepository = factureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            var factures = await _factureRepository
                .GetByIdsAsync(request.createPlanDePaiementRequest.FactureIDs);

            var plan = PlanDePaiement.Create(
                request.createPlanDePaiementRequest.MontantTotal,
                request.createPlanDePaiementRequest.NombreDeEcheances,
                DateTime.Now,
                request.createPlanDePaiementRequest.HasAdvance,
                request.createPlanDePaiementRequest.CreatedByUserID);

            plan.AddFactures(factures);

            await _planDePaiementRepository.AddAsync(plan);

            await _unitOfWork.CommitChangesAsync();

            return plan.Id;
        }
    }
}
