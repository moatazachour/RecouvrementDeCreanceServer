using MediatR;
using RdC.Application.Common.Interfaces;
using RdC.Domain.Litiges;

namespace RdC.Application.LitigeTypes.Commands.CreateLitigeType
{
    internal sealed class CreateLitigeTypeCommandHandler
        : IRequestHandler<CreateLitigeTypeCommand, int>
    {
        private readonly ILitigeTypeRepository _litigeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLitigeTypeCommandHandler(
            ILitigeTypeRepository litigeTypeRepository, 
            IUnitOfWork unitOfWork)
        {
            _litigeTypeRepository = litigeTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateLitigeTypeCommand request, CancellationToken cancellationToken)
        {
            var litigeType = LitigeType.Create(
                request.LitigeTypeName,
                request.LitigeTypeDescription);

            await _litigeTypeRepository.AddAsync(litigeType);

            await _unitOfWork.CommitChangesAsync();

            return litigeType.Id;
        }
    }
}
