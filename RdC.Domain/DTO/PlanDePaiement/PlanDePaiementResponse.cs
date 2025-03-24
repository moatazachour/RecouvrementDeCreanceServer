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
        decimal montantDeChaqueEcheance,
        decimal montantRestant,
        DateTime creationDate,
        PlanStatus planStatus,
        List<FactureResponse> Factures,
        List<PaiementDateResponse> PaiementDates);
}
