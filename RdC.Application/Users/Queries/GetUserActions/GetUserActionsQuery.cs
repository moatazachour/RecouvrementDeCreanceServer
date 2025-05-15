using MediatR;

namespace RdC.Application.Users.Queries.GetUserActions
{
    public record GetUserActionsQuery(int userID) : IRequest<int/*UserActionsResponse*/>;
}
