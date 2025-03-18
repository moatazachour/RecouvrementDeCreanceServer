using MediatR;

namespace RdC.Application.Acheteurs.Commands.AddAcheteurs
{
    public record AddAcheteursCommand() : IRequest<bool>;
}
