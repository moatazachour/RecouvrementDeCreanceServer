using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.LitigeType;

namespace RdC.Application.LitigeTypes.Queries.GetAllLitigeTypes
{
    internal sealed class GetAllLitigeTypesQueryHandler
        : IRequestHandler<GetAllLitigeTypesQuery, List<LitigeTypeResponse>>
    {
        private readonly ILitigeTypeRepository _litigeTypeRepository;

        public GetAllLitigeTypesQueryHandler(ILitigeTypeRepository litigeTypeRepository)
        {
            _litigeTypeRepository = litigeTypeRepository;
        }

        public async Task<List<LitigeTypeResponse>> Handle(GetAllLitigeTypesQuery request, CancellationToken cancellationToken)
        {
            var litigeTypes = await _litigeTypeRepository.GetAllAsync();

            var litigeTypesResponse = litigeTypes.Select(litigeType => new LitigeTypeResponse(
                litigeType.Id,
                litigeType.Name,
                litigeType.Description)).ToList();

            return litigeTypesResponse;
        }
    }
}
