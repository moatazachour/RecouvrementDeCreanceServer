using RdC.Domain.Abstrations;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;

namespace RdC.Domain.Paiements
{
    public sealed class Paiement : Entity
    {
        private Paiement(
            int id,
            int paiementDateID,
            decimal montantPayee,
            DateTime dateDePaiement)
            : base(id)
        {
            PaiementDateID = paiementDateID;
            MontantPayee = montantPayee;
            DateDePaiement = dateDePaiement;
        }

        public int PaiementDateID { get; private set; }
        public PaiementDate PaiementDate { get; private set; }

        public decimal MontantPayee { get; private set; }
        public DateTime DateDePaiement { get; private set; }

        public static Paiement CreatePaiement(
            int paiementDateID,
            decimal montantPayee,
            DateTime dateDePaiement)
        {
            Paiement paiement = new Paiement(
                id: 0,
                paiementDateID,
                montantPayee,
                dateDePaiement);

            return paiement;
        }

        private Paiement() { }
    }
}