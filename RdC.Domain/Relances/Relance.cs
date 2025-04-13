using RdC.Domain.Abstrations;
using RdC.Domain.PaiementDates;

namespace RdC.Domain.Relances
{
    public abstract class Relance : Entity
    {
        protected Relance(
            int id,
            int paiementDateID,
            bool isSent, 
            RelanceType relanceType, 
            DateTime dateDeEnvoie)
            : base(id)
        {
            PaiementDateID = paiementDateID;
            IsSent = isSent;
            RelanceType = relanceType;
            DateDeEnvoie = dateDeEnvoie;
        }

        public int PaiementDateID {  get; private set; }
        public PaiementDate PaiementDate { get; private set; }
        public bool IsSent { get; private set; }
        public RelanceType RelanceType { get; private set; }
        public DateTime DateDeEnvoie { get; private set; }

        protected Relance() { }
    }
}
