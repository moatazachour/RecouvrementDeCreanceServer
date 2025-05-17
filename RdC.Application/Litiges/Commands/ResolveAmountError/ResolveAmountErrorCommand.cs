using MediatR;

namespace RdC.Application.Litiges.Commands.ResolveAmountError
{
    public record ResolveAmountErrorCommand(
        int LitigeID,
        decimal CorrectedTotalAmount,
        decimal CorrectedAmountDue,
        int ResolutedByUserID) 
        : IRequest<bool>;
}
