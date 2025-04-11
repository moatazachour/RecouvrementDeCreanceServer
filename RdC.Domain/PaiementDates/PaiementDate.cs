using RdC.Domain.Abstrations;
using RdC.Domain.Paiements;
using RdC.Domain.PlanDePaiements;
using System.Text.Json.Serialization;

namespace RdC.Domain.PaiementDates
{
    public sealed class PaiementDate : Entity
    {
        public PaiementDate(
            int id,
            int planID,
            DateOnly echeanceDate,
            decimal montantDeEcheance,
            decimal montantPayee,
            decimal montantDue,
            bool isPaid,
            bool isLocked)
            : base(id)
        {
            PlanID = planID;
            EcheanceDate = echeanceDate;
            IsPaid = isPaid;
            MontantDeEcheance = montantDeEcheance;
            MontantPayee = montantPayee;
            MontantDue = montantDue;
            IsLocked = isLocked;
        }

        public int PlanID { get; private set; }
        public PlanDePaiement PlanDePaiement { get; private set; }

        [JsonIgnore]
        public List<Paiement> Paiements { get; private set; }

        public DateOnly EcheanceDate { get; set; }
        public decimal MontantDeEcheance {  get; private set; }
        public decimal MontantPayee { get; set; }
        public decimal MontantDue {  get; set; }
        public bool IsPaid { get; set; }
        public bool IsLocked { get; set; }


        public static PaiementDate Create(
            int PlanID,
            DateTime EcheanceDate,
            decimal MontantDeEcheance)
        {
            PaiementDate paiementDate = new PaiementDate(
                id: 0,
                PlanID,
                DateOnly.FromDateTime(EcheanceDate),
                MontantDeEcheance,
                montantPayee: 0,
                montantDue: MontantDeEcheance,
                isPaid: false,
                isLocked: false);

            return paiementDate;
        }
    }
}
