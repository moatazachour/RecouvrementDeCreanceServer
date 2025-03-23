using RdC.Domain.Abstrations;
using RdC.Domain.PlanDePaiements;

namespace RdC.Domain.PaiementDates
{
    public sealed class PaiementDate : Entity
    {
        public PaiementDate(
            int id,
            int planID,
            DateOnly echeanceDate,
            bool isPaid)
            : base(id)
        {
            PlanID = planID;
            EcheanceDate = echeanceDate;
            IsPaid = isPaid;
        }

        public int PlanID { get; private set; }
        public PlanDePaiement PlanDePaiement { get; private set; }

        public DateOnly EcheanceDate { get; private set; }
        public bool IsPaid { get; private set; }
    }
}
