using RdC.Domain.Acheteurs;
using System.Text.Json.Serialization;

namespace RdC.Domain.Factures
{
    public class Facture
    {
        public int FactureID { get; private set; }
        public string NumFacture { get; private set; }
        
        [JsonPropertyName("dateDeEcheance")]
        public DateOnly DateEcheance { get; private set; }
        public decimal MontantTotal { get; private set; }
        public decimal MontantRestantDue { get; private set; }
        public int AcheteurID { get; private set; }

        public Acheteur Acheteur { get; private set; }

        public Facture(int factureID, string numFacture, DateOnly dateEcheance, decimal montantTotal,
            decimal montantRestantDue, int acheteurID)
        {
            FactureID = factureID;
            NumFacture = numFacture;
            DateEcheance = dateEcheance;
            MontantTotal = montantTotal;
            MontantRestantDue = montantRestantDue;
            AcheteurID = acheteurID;
        }

        private Facture() { }
    }
}
