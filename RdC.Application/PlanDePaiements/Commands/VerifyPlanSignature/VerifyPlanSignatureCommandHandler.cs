using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Application.Common.Pdf;

namespace RdC.Application.PlanDePaiements.Commands.VerifyPlanSignature
{
    internal sealed class VerifyPlanSignatureCommandHandler
        : IRequestHandler<VerifyPlanSignatureCommand, bool>
    {
        private readonly IQuestPdfSignatureVerifier _questPdfSignatureVerifier;

        public VerifyPlanSignatureCommandHandler( 
            IQuestPdfSignatureVerifier questPdfSignatureVerifier)
        {
            _questPdfSignatureVerifier = questPdfSignatureVerifier;
        }

        public async Task<bool> Handle(VerifyPlanSignatureCommand request, CancellationToken cancellationToken)
        {
            return await _questPdfSignatureVerifier.HasValidSignature(request.planFile);
        }
    }
}
