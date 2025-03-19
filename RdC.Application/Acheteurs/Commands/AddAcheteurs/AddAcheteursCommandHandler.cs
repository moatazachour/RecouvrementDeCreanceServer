using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.Acheteurs.Commands.AddAcheteurs
{
    public class AddAcheteursCommandHandler : IRequestHandler<AddAcheteursCommand, bool>
    {
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAcheteursCommandHandler(IAcheteurRepository acheteurRepository, IUnitOfWork unitOfWork)
        {
            _acheteurRepository = acheteurRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddAcheteursCommand request, CancellationToken cancellationToken)
        {
            var isRefereshed = await _acheteurRepository.AddAcheteursAsync();
            await _unitOfWork.CommitChangesAsync();

            return isRefereshed;
        }
    }
}
