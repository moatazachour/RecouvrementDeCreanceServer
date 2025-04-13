using MediatR;
using RdC.Domain.PaiementDates;

namespace RdC.Application.PlanDePaiements.Commands.CheckPlanStatus
{
    public record CheckPlanStatusCommand(int planID, int maxUpaidPaiements) : IRequest<bool>;
}
