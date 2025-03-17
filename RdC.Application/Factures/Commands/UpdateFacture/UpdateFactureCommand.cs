using MediatR;
using RdC.Domain.DTO.Facture;
using RdC.Domain.Factures;

namespace RdC.Application.Factures.Commands.UpdateFacture
{
    public record UpdateFactureCommand(int FactureID, FactureUpdate factureUpdate) : IRequest<Facture>;
}
