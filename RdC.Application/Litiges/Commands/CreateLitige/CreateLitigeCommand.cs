using MediatR;
using RdC.Domain.DTO.Litige;

namespace RdC.Application.Litiges.Commands.CreateLitige
{
    public record CreateLitigeCommand(CreateLitigeRequest createLitigeRequest) : IRequest<int>;
}
