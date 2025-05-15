using RdC.Domain.DTO.Litige;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.DTO.PlanDePaiement;

namespace RdC.Domain.DTO.User
{
    public record UserActionsResponse(
        List<PlanDePaiementResponse> createdPlans,
        List<PlanDePaiementResponse> validatedPlans,
        List<LitigeResponse> declaredLitiges,
        List<LitigeResponse> resolutedLitiges,
        List<PaiementResponse> paidEcheances);
}
