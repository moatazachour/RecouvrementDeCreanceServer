using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.Facture;
using RdC.Domain.DTO.Litige;
using RdC.Domain.DTO.LitigeType;

namespace RdC.Application.Litiges.Queries.GetLitiges
{
    internal sealed class GetLitigesQueryHandler
        : IRequestHandler<GetLitigesQuery, List<LitigeResponse>>
    {
        private readonly ILitigeRepository _litigeRepository;

        public GetLitigesQueryHandler(ILitigeRepository litigeRepository)
        {
            _litigeRepository = litigeRepository;
        }

        public async Task<List<LitigeResponse>> Handle(GetLitigesQuery request, CancellationToken cancellationToken)
        {
            var litiges = await _litigeRepository.GetAllAsync();

            var litigesResponse = litiges.Select(litige => new LitigeResponse(
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
                                            litige.ResolutionDate)).ToList();

            return litigesResponse;
        }
    }
}
