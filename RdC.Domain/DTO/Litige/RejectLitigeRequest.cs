namespace RdC.Domain.DTO.Litige
{
    public record RejectLitigeRequest(
        int litigeID,
        int rejectedByUserID);
}
