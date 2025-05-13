using MediatR;

namespace RdC.Application.Users.Commands.CompleteUserRegistration
{
    public record CompleteUserRegistrationCommand(
        string userEmail,
        string username,
        string password)
        : IRequest<bool>;
}
