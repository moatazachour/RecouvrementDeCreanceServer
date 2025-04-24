using RdC.Domain.Abstrations;
using RdC.Domain.Factures;
using System.Text.Json.Serialization;

namespace RdC.Domain.Litiges
{
    public sealed class Litige : Entity
    {
        public Litige(
            int id,
            int factureID,
            int litigeTypeID,
            LitigeStatus litigeStatus,
            string litigeDescription,
            DateTime creationDate)
            : base(id)
        {
            FactureID = factureID;
            LitigeTypeID = litigeTypeID;
            LitigeStatus = litigeStatus;
            LitigeDescription = litigeDescription;
            CreationDate = creationDate;
        }

        public int FactureID { get; private set; }
        public Facture Facture { get; private set; }

        public int LitigeTypeID { get; private set; }
        public LitigeType LitigeType { get; private set; }

        public LitigeStatus LitigeStatus { get; private set; }
        public string LitigeDescription { get; private set; }
        public DateTime CreationDate { get; private set; }

        public DateTime? ResolutionDate { get; private set; }

        [JsonIgnore]
        public List<LitigeJustificatif> Justificatifs { get; private set; } = new();

        public static Litige Declare(
            int factureID,
            int litigeTypeID,
            string litigeDescription)
        {
            var litige = new Litige(
                id: 0,
                factureID: factureID,
                litigeTypeID: litigeTypeID,
                litigeStatus: LitigeStatus.EN_COURS,
                litigeDescription: litigeDescription,
                creationDate: DateTime.Now);

            return litige;
        }

        public void AddJustificatif(string fileName, string filePath)
        {
            var justificatif = LitigeJustificatif.Upload(
                                    LitigeID: Id,
                                    NomFichier: fileName,
                                    CheminFichier: filePath);

            Justificatifs.Add(justificatif);
        }

        public void Reject()
        {
            LitigeStatus = LitigeStatus.REJETE;

            ResolutionDate = DateTime.Now;
        }

        public void Accept()
        {
            LitigeStatus = LitigeStatus.RESOLU;

            ResolutionDate = DateTime.Now;
        }
    }
}
