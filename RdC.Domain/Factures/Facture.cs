using RdC.Domain.Abstrations;
using RdC.Domain.Acheteurs;
using RdC.Domain.Litiges;
using RdC.Domain.PlanDePaiements;
using System.Text.Json.Serialization;

namespace RdC.Domain.Factures
{
    public class Facture : Entity
    {
        public Facture(
            int Id,
            string numFacture,
            DateOnly dateEcheance,
            decimal montantTotal,
            decimal montantRestantDue,
            int acheteurID)
            : base(Id)
        {
            NumFacture = numFacture;
            DateEcheance = dateEcheance;
            MontantTotal = montantTotal;
            MontantRestantDue = montantRestantDue;
            AcheteurID = acheteurID;
        }

        public string NumFacture { get; private set; }
        public DateOnly DateEcheance { get; private set; }
        public decimal MontantTotal { get; private set; }
        public decimal MontantRestantDue { get; set; }
        public FactureStatus Status { get; set; }
        public int AcheteurID { get; private set; }

        public Acheteur Acheteur { get; private set; }

        public List<PlanDePaiement> PlanDePaiements { get; private set; } = new();

        [JsonIgnore]
        public List<Litige> Litiges { get; private set; } = new();

        private Facture() { }

        public void GetFactureStatus()
        {
            if (MontantRestantDue == 0)
            {
                Status = FactureStatus.PAYEE;
            }

            if (MontantRestantDue == MontantTotal)
            {
                Status = FactureStatus.IMPAYEE;
            }

            if (MontantRestantDue <  MontantTotal)
            {
                Status = FactureStatus.PARTIELLEMENT_PAYEE;
            }
        }

        public void CorrectFactureAmounts(
            decimal correctedMontantTotal,
            decimal correctedMontantDue)
        {
            MontantTotal = correctedMontantTotal;
            MontantRestantDue = correctedMontantDue;
        }
    }
}
