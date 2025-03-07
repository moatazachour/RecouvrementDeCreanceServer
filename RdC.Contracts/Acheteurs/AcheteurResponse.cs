namespace RdC.Contracts.Acheteurs
{
    public record AcheteurResponse(int AcheteurID, string Nom, string Prenom, string? Adresse, string Email, string Telephone);
}
