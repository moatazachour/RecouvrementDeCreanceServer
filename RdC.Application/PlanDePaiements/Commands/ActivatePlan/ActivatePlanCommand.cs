using MediatR;

namespace RdC.Application.PlanDePaiements.Commands.ActivatePlan
{
    public record ActivatePlanCommand(int planID) : IRequest<bool>;
}
