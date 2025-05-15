using RdC.Domain.DTO.Litige;
using RdC.Domain.DTO.Paiement;
using RdC.Domain.DTO.PlanDePaiement;

namespace RdC.Domain.DTO.User
{
    public record UserActionsResponse(
        List<PlanDePaiementBasicResponse> createdPlans,
        List<PlanDePaiementBasicResponse> validatedPlans,
        List<LitigeBasicResponse> declaredLitiges,
        List<LitigeBasicResponse> resolutedLitiges,
        List<PaiementResponse> paidEcheances);
}
