using MediatR;
using RdC.Domain.DTO.User;

namespace RdC.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery() : IRequest<List<UserResponse>>;
}
