﻿using RdC.Domain.DTO.Facture;

namespace RdC.Domain.DTO.Acheteur
{
    public record AcheteurResponse(int AcheteurID, string Nom, string Prenom, string? Adresse, string Email, string Telephone,
        ICollection<FactureResponse> Factures);
}
