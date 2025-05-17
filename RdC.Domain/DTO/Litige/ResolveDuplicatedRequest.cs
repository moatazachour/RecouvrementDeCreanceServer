namespace RdC.Domain.DTO.Litige
{
    public record ResolveDuplicatedRequest(
        int litigeID,
        int resolvedByUserID);
}
