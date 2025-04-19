using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.Litige;
using RdC.Domain.DTO.LitigeType;
using RdC.Domain.Litiges;

namespace RdC.Application.Litiges.Queries.GetLitige
{
    internal sealed class GetLitigeQueryHandler
        : IRequestHandler<GetLitigeQuery, LitigeResponse?>
    {
        private readonly ILitigeRepository _litigeRepository;

        public GetLitigeQueryHandler(ILitigeRepository litigeRepository)
        {
            _litigeRepository = litigeRepository;
        }

        public async Task<LitigeResponse?> Handle(GetLitigeQuery request, CancellationToken cancellationToken)
        {
            var litige = await _litigeRepository.GetByIdAsync(request.litigeID);

            if (litige == null)
                return null;

            var litigeResponse = new LitigeResponse(
                                            litige.Id,
                                            new FactureResponse(
                                                litige.FactureID,
                                                litige.Facture.NumFacture,
                                                litige.Facture.DateEcheance,
                                                litige.Facture.MontantTotal,
                                                litige.Facture.MontantRestantDue,
                                                litige.Facture.AcheteurID,
                                                litige.Facture.Status),
                                            new LitigeTypeResponse(
                                                litige.LitigeTypeID,
                                                litige.LitigeType.Name,
                                                litige.LitigeType.Description),
                                            litige.LitigeStatus,
                                            litige.LitigeDescription,
                                            litige.CreationDate,
                                            litige.ResolutionDate);

            return litigeResponse;
        }
    }
}
