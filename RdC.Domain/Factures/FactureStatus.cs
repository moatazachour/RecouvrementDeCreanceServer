using System.Text.Json.Serialization;

namespace RdC.Domain.Factures
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FactureStatus
    {
        Impayee,
        PartiellementPayee,
        Payee,
        EnLitige
    }
}
