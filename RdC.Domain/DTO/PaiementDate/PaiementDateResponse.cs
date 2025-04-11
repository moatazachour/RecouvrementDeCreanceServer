using RdC.Domain.DTO.Paiement;

namespace RdC.Domain.DTO.PaiementDate
{
    public record PaiementDateResponse(
        int DateID,
        int PlanID,
        DateOnly EcheanceDate,
        decimal MontantDeEcheance,
        decimal MontantPayee,
        decimal MontantDue,
        bool IsPaid,
        bool IsLocked,
        List<PaiementResponse> PaiementResponses = null!);
}
