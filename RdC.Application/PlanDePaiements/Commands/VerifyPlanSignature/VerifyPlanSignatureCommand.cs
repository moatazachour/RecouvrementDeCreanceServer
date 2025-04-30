using MediatR;

namespace RdC.Application.PlanDePaiements.Commands.VerifyPlanSignature
{
    public record VerifyPlanSignatureCommand(
        byte[] planFile)
        : IRequest<bool>;
}
