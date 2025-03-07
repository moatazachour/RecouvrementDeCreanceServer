namespace RdC.Contracts.Acheteurs
{
    public record CreateAcheteurRequest(string Nom, string Prenom, string? Adresse, string Email, string Telephone);
}
