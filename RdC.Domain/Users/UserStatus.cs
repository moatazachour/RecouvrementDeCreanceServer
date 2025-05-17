using System.Text.Json.Serialization;

namespace RdC.Domain.Users
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserStatus
    {
        EN_ATTENTE,
        ACTIVE,
        INACTIVE
    }
}
