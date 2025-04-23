namespace RdC.Domain.DTO.Litige
{
    public record JustificatifResponse(
        int JustificatifID,
        int LitigeID,
        string NomFichier,
        string CheminFichier,
        DateTime DateAjout);
}
