using RdC.Domain.Factures;

namespace RdC.Domain.DTO.Facture
{
    public record FactureUpdate(decimal MontantRestantDue, FactureStatus Status);
}
