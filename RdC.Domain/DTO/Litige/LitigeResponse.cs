using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.LitigeType;
using RdC.Domain.Litiges;

namespace RdC.Domain.DTO.Litige
{
    public record LitigeResponse(
        int LitigeID,
        FactureResponse Facture,
        LitigeTypeResponse Type,
        LitigeStatus LitigeStatus,
        string LitigeDescription,
        DateTime CreationDate,
        DateTime? ResolutionDate,
        List<JustificatifResponse> LitigeJustificatifs);
}
