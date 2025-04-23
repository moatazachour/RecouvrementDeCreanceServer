using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Litiges;

namespace RdC.Application.Litiges.Queries.GetJustificatif
{
    internal sealed class GetJustificatifQueryHandler
        : IRequestHandler<GetJustificatifQuery, LitigeJustificatif?>
    {
        private readonly ILitigeJustificatifRepository _litigeJustificatifRepository;

        public GetJustificatifQueryHandler(
            ILitigeJustificatifRepository litigeJustificatifRepository)
        {
            _litigeJustificatifRepository = litigeJustificatifRepository;
        }

        public async Task<LitigeJustificatif?> Handle(GetJustificatifQuery request, CancellationToken cancellationToken)
        {
            return await _litigeJustificatifRepository.GetByIdAsync(request.JustificatifID);
        }
    }
}
