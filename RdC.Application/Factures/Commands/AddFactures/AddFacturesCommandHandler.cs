using MediatR;
using RdC.Application.Common.Interfaces;

namespace RdC.Application.Factures.Commands.AddFactures
{
    public class AddFacturesCommandHandler : IRequestHandler<AddFacturesCommand, bool>
    {
        private readonly IFactureRepository _factureRepository;
        private readonly IAcheteurRepository _acheteurRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddFacturesCommandHandler(IFactureRepository factureRepository, IAcheteurRepository acheteurRepository,
            IUnitOfWork unitOfWork)
        {
            _factureRepository = factureRepository;
            _acheteurRepository = acheteurRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddFacturesCommand request, CancellationToken cancellationToken)
        {
            bool refreshAcheteurs = await _acheteurRepository.AddAcheteursAsync();
            bool refreshFactures = await _factureRepository.AddFacturesAsync();

            if (!refreshAcheteurs || !refreshFactures)
                return false;

            await _unitOfWork.CommitChangesAsync();

            return true;
        }
    }
}
