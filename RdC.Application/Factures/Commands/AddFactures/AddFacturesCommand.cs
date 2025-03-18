using MediatR;

namespace RdC.Application.Factures.Commands.AddFactures
{
    public record AddFacturesCommand() : IRequest<bool>;
}
