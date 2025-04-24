using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.LitigeType;
using RdC.Domain.Litiges;

namespace RdC.Application.LitigeTypes.Queries.GetLitigeType
{
    internal sealed class GetLitigeTypeQueryHandler
        : IRequestHandler<GetLitigeTypeQuery, LitigeTypeResponse?>
    {
        private readonly ILitigeTypeRepository _litigeTypeRepository;

        public GetLitigeTypeQueryHandler(ILitigeTypeRepository litigeTypeRepository)
        {
            _litigeTypeRepository = litigeTypeRepository;
        }

        public async Task<LitigeTypeResponse?> Handle(GetLitigeTypeQuery request, CancellationToken cancellationToken)
        {
            var litigeType = await _litigeTypeRepository.GetByIdAsync(request.LitigeTypeID);

            if (litigeType is null)
                return null;

            return new LitigeTypeResponse(
                litigeType.Id,
                litigeType.Name,
                litigeType.Description);
        }
    }
}
