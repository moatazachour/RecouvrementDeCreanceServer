using System.Text.Json.Serialization;

namespace RdC.Contracts.Factures
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
