using RdC.Domain.Abstrations;
using RdC.Domain.Acheteurs;
using System.Text.Json.Serialization;

namespace RdC.Domain.Factures
{
    public class Facture : Entity
    {
        public string NumFacture { get; private set; }

        [JsonPropertyName("dateDeEcheance")]
        public DateOnly DateEcheance { get; private set; }
        public decimal MontantTotal { get; private set; }
        public decimal MontantRestantDue { get; set; }
        public FactureStatus Status { get; set; }
        public int AcheteurID { get; private set; }

        public Acheteur Acheteur { get; private set; }

        public Facture(int Id, string numFacture, DateOnly dateEcheance, decimal montantTotal,
            decimal montantRestantDue, int acheteurID, FactureStatus status)
            : base(Id)
        {
            NumFacture = numFacture;
            DateEcheance = dateEcheance;
            MontantTotal = montantTotal;
            MontantRestantDue = montantRestantDue;
            AcheteurID = acheteurID;
            Status = status;
        }

        private Facture() { }
    }
}
