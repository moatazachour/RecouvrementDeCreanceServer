﻿using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.DTO.PaiementDate;
using RdC.Domain.DTO.PlanDePaiement;

namespace RdC.Application.PlanDePaiements.Queries.GetPlan
{
    internal sealed class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanDePaiementResponse?>
    {
        private readonly IPlanDePaiementRepository _planDePaiementRepository;

        public GetPlanQueryHandler(IPlanDePaiementRepository planDePaiementRepository)
        {
            _planDePaiementRepository = planDePaiementRepository;
        }

        public async Task<PlanDePaiementResponse?> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var planDePaiement = await _planDePaiementRepository.GetByIdAsync(request.PlanID);

            if (planDePaiement is null)
                return null;

            return new PlanDePaiementResponse(
                planDePaiement.Id,
                planDePaiement.MontantTotal,
                planDePaiement.NombreDeEcheances,
                planDePaiement.MontantRestant,
                planDePaiement.CreationDate,
                planDePaiement.PlanStatus,
                planDePaiement.IsLocked,
                planDePaiement.HasAdvance,
                planDePaiement.CreatedByUserID,
                planDePaiement.Factures.Select(facture => new FactureResponse(
                                                            facture.Id,
                                                            facture.NumFacture,
                                                            facture.DateEcheance,
                                                            facture.MontantTotal,
                                                            facture.MontantRestantDue,
                                                            facture.AcheteurID,
                                                            facture.Status)).ToList(),
                planDePaiement.PaiementsDates.Select(paiementDate => new PaiementDateResponse(
                                                            paiementDate.Id,
                                                            paiementDate.PlanID,
                                                            paiementDate.EcheanceDate,
                                                            paiementDate.MontantDeEcheance,
                                                            paiementDate.MontantPayee,
                                                            paiementDate.MontantDue,
                                                            paiementDate.IsPaid,
                                                            paiementDate.IsLocked
                                                            )).ToList());
        }
    }
}
