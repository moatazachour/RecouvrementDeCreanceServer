using MediatR;
using RdC.Domain.DTO.Litige;

namespace RdC.Application.Litiges.Commands.UploadLitigeJustificatifs
{
    public record UploadLitigeJustificatifsCommand(
        int LitigeID,
        List<FileDto> Files) : IRequest<Unit>;
}
