namespace RdC.Domain.DTO.User
{
    public record CompleteRegistrationRequest(
        string userEmail,
        string username,
        string password);
}
