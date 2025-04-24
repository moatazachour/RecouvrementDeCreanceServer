using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.DTO.LitigeType;

namespace RdC.Application.LitigeTypes.Commands.UpdateLitigeType
{
    internal sealed class UpdateLitigeTypeCommandHandler
        : IRequestHandler<UpdateLitigeTypeCommand, LitigeTypeResponse?>
    {
        private readonly ILitigeTypeRepository _litigeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLitigeTypeCommandHandler(
            ILitigeTypeRepository litigeTypeRepository, 
            IUnitOfWork unitOfWork)
        {
            _litigeTypeRepository = litigeTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LitigeTypeResponse?> Handle(UpdateLitigeTypeCommand request, CancellationToken cancellationToken)
        {
            var litigeType = await _litigeTypeRepository.GetByIdAsync(request.LitigeTypeID);

            if (litigeType is null)
            {
                return null;
            }

            litigeType.Name = request.LitigeTypeName;
            litigeType.Description = request.LitigeTypeDescription;

            await _unitOfWork.CommitChangesAsync();

            return new LitigeTypeResponse(
                litigeType.Id,
                litigeType.Name,
                litigeType.Description);
        }
    }
}
