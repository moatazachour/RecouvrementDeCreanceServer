using RdC.Domain.Abstrations;

namespace RdC.Domain.Litiges
{
    public class LitigeJustificatif : Entity
    {
        private LitigeJustificatif(
            int id,
            int litigeID,
            string nomFichier,
            string cheminFichier,
            DateTime dateAjout)
            : base(id)
        {
            LitigeID = litigeID;
            NomFichier = nomFichier;
            CheminFichier = cheminFichier;
            DateAjout = dateAjout;
        }

        public int LitigeID { get; private set; }
        public Litige Litige { get; private set; }

        public string NomFichier { get; private set; }
        public string CheminFichier { get; private set; }
        public DateTime DateAjout { get; private set; }

        public static LitigeJustificatif Upload(
            int LitigeID,
            string NomFichier,
            string CheminFichier)
        {
            return new LitigeJustificatif(
                id: 0,
                litigeID: LitigeID,
                nomFichier: NomFichier,
                cheminFichier: CheminFichier,
                dateAjout: DateTime.Now);
        }

        private LitigeJustificatif() { }
    }
}
