namespace RdC.Api.Controllers.Litiges
{
    public record CorrectAmountRequest(
        decimal correctedMontantTotal,
        decimal correctedAmountDue,
        int correctedByUserID);
}
