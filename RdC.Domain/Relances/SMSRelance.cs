namespace RdC.Domain.Relances
{
    public class SMSRelance : Relance
    {
        private SMSRelance(
            int id,
            int paiementDateID,
            bool isSent,
            RelanceType relanceType,
            DateTime dateDeEnvoie,
            string telephone,
            string smsBody)
            : base(id,
                  paiementDateID,
                  isSent,
                  relanceType,
                  dateDeEnvoie)
        {
            Telephone = telephone;
            SMSBody = smsBody;
        }

        public string Telephone { get; private set; }
        public string SMSBody { get; private set; }

        public static SMSRelance Send(
            int paiementDateID,
            string telephone,
            string smsBody)
        {
            if (string.IsNullOrWhiteSpace(smsBody))
                throw new ArgumentException("SMS cannot be empty");

            var sms = new SMSRelance(
                id: 0,
                paiementDateID,
                isSent: true,
                RelanceType.SMS,
                DateTime.Now,
                telephone,
                smsBody);

            return sms;
        }

        private SMSRelance() { }
    }
}
