using System.Text.Json.Serialization;

namespace RdC.Domain.PlanDePaiements
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PlanStatus
    {
        EN_ATTENTE,
        EN_COURS,
        TERMINE,
        ANNULE
    }
}
