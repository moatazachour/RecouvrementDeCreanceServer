using MediatR;
using RdC.Domain.DTO.User;

namespace RdC.Application.Users.Queries.GetUserActions
{
    public record GetUserActionsQuery(int userID) : IRequest<UserActionsResponse>;
}
