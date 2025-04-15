namespace RdC.Domain.DTO.PaiementDate
{
    public record CreatePaiementDatesRequest(
        List<PaiementDateSimpleResponse> PaiementDates);
}
