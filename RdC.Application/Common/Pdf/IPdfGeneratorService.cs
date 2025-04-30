using RdC.Domain.Acheteurs;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;

namespace RdC.Application.Common.Pdf
{
    public interface IPdfGeneratorService
    {
        byte[] GeneratePlanDePaiementPdf(
            PlanDePaiement plan, 
            List<PaiementDate> paiementDates,
            Acheteur acheteur);
    }
}
