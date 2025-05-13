using MediatR;
using RdC.Domain.DTO.User;

namespace RdC.Application.Users.Queries.GetUser
{
    public record GetUserQuery(
        int userID)
        : IRequest<UserResponse?>;
}
