using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.PaiementDate;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;

namespace RdC.Domain.DTO.PlanDePaiement
{
    public record PlanDePaiementResponse(
        int PlanID,
        decimal MontantTotal,
        byte nombreDeEcheances,
        decimal montantRestant,
        DateTime creationDate,
        PlanStatus planStatus,
        bool isLocked,
        bool hasAdvance,
        int CreatedByUserID,
        List<FactureResponse> Factures,
        List<PaiementDateResponse> PaiementDates);
}
