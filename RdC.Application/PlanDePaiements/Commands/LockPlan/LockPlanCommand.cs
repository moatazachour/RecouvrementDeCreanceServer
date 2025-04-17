using MediatR;

namespace RdC.Application.PlanDePaiements.Commands.LockPlan
{
    public record LockPlanCommand(int PlanID) : IRequest<bool>;
}
