using MediatR;
using RdC.Domain.DTO.PlanDePaiement;

namespace RdC.Application.PlanDePaiements.Queries.ListPlans
{
    public record ListPlansQuery() : IRequest<List<PlanDePaiementResponse>>;
}
