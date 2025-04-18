using System.Text.Json.Serialization;

namespace RdC.Domain.Litiges
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LitigeStatus
    {
        EN_ATTENTE,
        EN_COURS,
        RESOLU,
        REJETE,
        CLOS
    }
}
