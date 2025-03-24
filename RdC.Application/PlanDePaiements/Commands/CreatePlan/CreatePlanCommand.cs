using MediatR;
using RdC.Domain.DTO.PlanDePaiement;

namespace RdC.Application.PlanDePaiements.Commands.CreatePlan
{
    public record CreatePlanCommand(
        CreatePlanDePaiementRequest createPlanDePaiementRequest) : IRequest<int>;
}
