namespace RdC.Domain.DTO.User
{
    public record AddUserRequest(
        string email, 
        int roleID);
}
