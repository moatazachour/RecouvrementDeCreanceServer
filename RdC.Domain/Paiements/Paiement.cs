using RdC.Domain.Abstrations;
using RdC.Domain.PaiementDates;
using RdC.Domain.PlanDePaiements;
using RdC.Domain.Users;

namespace RdC.Domain.Paiements
{
    public sealed class Paiement : Entity
    {
        private Paiement(
            int id,
            int paiementDateID,
            decimal montantPayee,
            DateTime dateDePaiement,
            int paidByUserID)
            : base(id)
        {
            PaiementDateID = paiementDateID;
            MontantPayee = montantPayee;
            DateDePaiement = dateDePaiement;
            PaidByUserID = paidByUserID;
        }

        public int PaiementDateID { get; private set; }
        public PaiementDate PaiementDate { get; private set; }

        public decimal MontantPayee { get; private set; }
        public DateTime DateDePaiement { get; private set; }

        public int PaidByUserID { get; private set; }
        public User User { get; private set; }

        public static Paiement CreatePaiement(
            int paiementDateID,
            decimal montantPayee,
            int paidByUserID)
        {
            Paiement paiement = new Paiement(
                id: 0,
                paiementDateID,
                montantPayee,
                DateTime.Now,
                paidByUserID);

            return paiement;
        }

        private Paiement() { }
    }
}