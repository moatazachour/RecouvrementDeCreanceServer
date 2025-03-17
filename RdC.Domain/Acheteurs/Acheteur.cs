using RdC.Domain.Factures;
using System.Text.Json.Serialization;

namespace RdC.Domain.Acheteurs
{
    public class Acheteur
    {
        public int AcheteurID { get; private set; }
        public string Nom { get; private set; }
        public string Prenom { get; private set; }
        public string? Adresse { get; private set; }
        public string Email { get; private set; }
        public string Telephone { get; private set; }

        [JsonIgnore]
        public ICollection<Facture> Factures { get; private set; }


        public Acheteur(int acheteurID, string nom, string prenom, string? adresse, string email, string telephone)
        {
            AcheteurID = acheteurID;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Email = email;
            Telephone = telephone;
            Factures = new List<Facture>();
        }

        private Acheteur() { }
    }
}
