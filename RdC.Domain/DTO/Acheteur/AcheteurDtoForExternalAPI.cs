namespace RdC.Domain.DTO.Acheteur
{
    public record AcheteurDtoForExternalAPI(
        int AcheteurID, 
        string Nom, 
        string Prenom, 
        string? Adresse, 
        string Email, 
        string Telephone);
}
