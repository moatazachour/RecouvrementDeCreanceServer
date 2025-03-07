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

        public Acheteur(int acheteurID, string nom, string prenom, string? adresse, string email, string telephone)
        {
            AcheteurID = acheteurID;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Email = email;
            Telephone = telephone;
        }

        // Allow EF Core to create via reflexion an instance of the subscription
        // same reason of adding private set
        private Acheteur()
        {

        }
    }
}
