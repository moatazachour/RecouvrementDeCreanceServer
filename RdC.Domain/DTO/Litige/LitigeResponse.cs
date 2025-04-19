using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.LitigeType;
using RdC.Domain.Factures;
using RdC.Domain.Litiges;

namespace RdC.Domain.DTO.Litige
{
    public record LitigeResponse(
        int LitigeID,
        FactureResponse Facture,
        LitigeTypeResponse TypeID,
        LitigeStatus LitigeStatus,
        string LitigeDescription,
        DateTime CreationDate,
        DateTime? ResolutionDate);
}
