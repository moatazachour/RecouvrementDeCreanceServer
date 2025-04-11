using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.DTO.PaiementDate;
using RdC.Domain.DTO.PlanDePaiement;

namespace RdC.Application.PlanDePaiements.Queries.ListPlans
{
    internal sealed class ListPlansQueryHandler : IRequestHandler<ListPlansQuery, List<PlanDePaiementResponse>>
    {
        public readonly IPlanDePaiementRepository _planDePaiementRepository;

        public ListPlansQueryHandler(IPlanDePaiementRepository planDePaiementRepository)
        {
            _planDePaiementRepository = planDePaiementRepository;
        }

        public async Task<List<PlanDePaiementResponse>> Handle(ListPlansQuery request, CancellationToken cancellationToken)
        {
            var listDePlan = await _planDePaiementRepository.GetAllAsync();

            var listDePlanResponse = listDePlan.Select(p => new PlanDePaiementResponse(
                                            p.Id,
                                            p.MontantTotal,
                                            p.NombreDeEcheances,
                                            p.MontantDeChaqueEcheance,
                                            p.MontantRestant,
                                            p.CreationDate,
                                            p.PlanStatus,
                                            p.Factures.Select(facture => new FactureResponse(
                                                                                facture.Id,
                                                                                facture.NumFacture,
                                                                                facture.DateEcheance,
                                                                                facture.MontantTotal,
                                                                                facture.MontantRestantDue,
                                                                                facture.AcheteurID,
                                                                                facture.Status)).ToList(),
                                            p.PaiementsDates.Select(paiementDate => new PaiementDateResponse(
                                                                                paiementDate.Id,
                                                                                paiementDate.PlanID,
                                                                                paiementDate.EcheanceDate,
                                                                                paiementDate.MontantDeEcheance,
                                                                                paiementDate.MontantPayee,
                                                                                paiementDate.MontantDue,
                                                                                paiementDate.IsPaid,
                                                                                paiementDate.IsLocked
                                                                                )).ToList()))
                                    .ToList();

            return listDePlanResponse;
        }
    }
}
