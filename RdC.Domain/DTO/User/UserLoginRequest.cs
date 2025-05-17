namespace RdC.Domain.DTO.User
{
    public record UserLoginRequest(
        string Identifier,
        string Password);
}
