using RdC.Domain.Abstrations;
using RdC.Domain.PlanDePaiements;

namespace RdC.Domain.Paiements
{
    public sealed class Paiement : Entity
    {
        private Paiement(
            int id,
            int planID,
            decimal montantPayee,
            DateTime dateDePaiement)
            : base(id)
        {
            PlanID = planID;
            MontantPayee = montantPayee;
            DateDePaiement = dateDePaiement;
        }

        public int PlanID { get; private set; }
        public PlanDePaiement PlanDePaiement { get; private set; }

        public decimal MontantPayee { get; private set; }
        public DateTime DateDePaiement { get; private set; }

        public static Paiement CreatePaiement(
            int planID,
            decimal montantPayee,
            DateTime dateDePaiement)
        {
            Paiement paiement = new Paiement(
                id: 0,
                planID,
                montantPayee,
                dateDePaiement);

            return paiement;
        }
    }
}