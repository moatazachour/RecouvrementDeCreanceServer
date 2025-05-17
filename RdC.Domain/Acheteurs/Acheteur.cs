using RdC.Domain.Abstrations;
using RdC.Domain.Factures;
using System.Text.Json.Serialization;

namespace RdC.Domain.Acheteurs
{
    public sealed class Acheteur : Entity
    {
        public string Nom { get; private set; }
        public string Prenom { get; private set; }
        public string? Adresse { get; private set; }
        public string Email { get; private set; }
        public string Telephone { get; private set; }
        public float Score { get; private set; }

        [JsonIgnore]
        public ICollection<Facture> Factures { get; private set; }


        public Acheteur(
            int Id, 
            string nom, 
            string prenom, 
            string? adresse, 
            string email, 
            string telephone, 
            float score)
            : base(Id)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Email = email;
            Telephone = telephone;
            Factures = new List<Facture>();
            Score = score;
        }

        private Acheteur() { }
    }
}
