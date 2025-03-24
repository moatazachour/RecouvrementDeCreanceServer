using MediatR;
using RdC.Domain.DTO.PlanDePaiement;
using RdC.Domain.PlanDePaiements;

namespace RdC.Application.PlanDePaiements.Queries.GetPlan
{
    public record GetPlanQuery(int PlanID) : IRequest<PlanDePaiementResponse?>;
}
