namespace RdC.Domain.DTO.Litige
{
    public record CreateLitigeRequest(
        int FactureID,
        int TypeID,
        string LitigeDescription,
        int DeclaredByUserID);
}
