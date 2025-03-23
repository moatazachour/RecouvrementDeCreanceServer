using RdC.Domain.Abstrations;
using RdC.Domain.Factures;
using RdC.Domain.PaiementDates;
using System.Text.Json.Serialization;

namespace RdC.Domain.PlanDePaiements
{
    public sealed class PlanDePaiement : Entity
    {
        private PlanDePaiement(
            int id,
            decimal montantTotal, 
            byte nombreDeEcheances, 
            decimal montantDeChaqueEcheance, 
            decimal montantRestant,
            DateTime creationDate,
            PlanStatus planStatus)
            : base(id)
        {
            MontantTotal = montantTotal;
            NombreDeEcheances = nombreDeEcheances;
            MontantDeChaqueEcheance = montantDeChaqueEcheance;
            MontantRestant = montantRestant;
            CreationDate = creationDate;
            PlanStatus = planStatus;
        }
        [JsonIgnore]
        public List<Facture> Factures = new();

        [JsonIgnore]
        public List<PaiementDate> PaiementsDates = new();

        public decimal MontantTotal { get; private set; }
        public byte NombreDeEcheances { get; private set; }
        public decimal MontantDeChaqueEcheance {  get; private set; }
        public decimal MontantRestant { get; private set; }
        public DateTime CreationDate { get; private set; }
        public PlanStatus PlanStatus { get; private set; }

        public static PlanDePaiement Create(
            decimal montantTotal,
            byte nombreDeEcheances,
            decimal montantDeChaqueEcheance,
            decimal montantRestant,
            DateTime creationDate)
        {
            var plan = new PlanDePaiement(
                id: 0,
                montantTotal,
                nombreDeEcheances,
                montantDeChaqueEcheance,
                montantRestant,
                creationDate,
                PlanStatus.EnCours);

            return plan;
        }
    }
}
