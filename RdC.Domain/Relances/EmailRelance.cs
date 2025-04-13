using RdC.Domain.PaiementDates;

namespace RdC.Domain.Relances
{
    public class EmailRelance : Relance
    {
        private EmailRelance(
            int id,
            int paiementDateID,
            bool isSent,
            RelanceType relanceType,
            DateTime dateDeEnvoie,
            string email,
            string emailBody)
            : base(id,
                  paiementDateID,
                  isSent,
                  relanceType,
                  dateDeEnvoie)
        {
            Email = email;
            EmailBody = emailBody;
        }

        public string Email { get; private set; }
        public string EmailBody { get; private set; }

        public static EmailRelance Send(
            int paiementDateID,
            string email,
            string emailBody)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");

            if (!email.Contains("@"))
                throw new ArgumentException("Invalid email");


            var emailRelance = new EmailRelance(
                id: 0,
                paiementDateID,
                isSent: true,
                RelanceType.Email,
                DateTime.Now,
                email,
                emailBody);

            return emailRelance;
        }

        private EmailRelance() { }
    }
}
