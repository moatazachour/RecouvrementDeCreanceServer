using System.Text.Json.Serialization;

namespace RdC.Domain.Relances
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RelanceType
    {
        Email,
        SMS
    }
}
