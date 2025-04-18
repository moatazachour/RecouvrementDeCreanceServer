using System.Text.Json.Serialization;

namespace RdC.Domain.Factures
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FactureStatus
    {
        IMPAYEE,
        PARTIELLEMENT_PAYEE,
        PAYEE,
        EN_LITIGE,
        EN_COURS_DE_PAIEMENT
    }
}
