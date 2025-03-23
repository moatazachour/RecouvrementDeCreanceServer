using System.Text.Json.Serialization;

namespace RdC.Domain.PlanDePaiements
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PlanStatus
    {
        EnCours = 0,
        Termine = 1,
        Annule = 2
    }
}
