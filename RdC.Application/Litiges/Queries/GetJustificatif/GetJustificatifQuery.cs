using MediatR;
using RdC.Domain.Litiges;

namespace RdC.Application.Litiges.Queries.GetJustificatif
{
    public record GetJustificatifQuery(int JustificatifID) : IRequest<LitigeJustificatif?>;
}
